using UnityEngine;
using System.Collections.Generic;
using System;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("AudioManager");
                    instance = go.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(0.1f, 3f)]
        public float pitch = 1f;
        public bool loop = false;
        public AudioSource source;
    }

    public Sound[] sounds;
    public float masterVolume = 1f;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    [SerializeField]private AudioSource musicSource;
    private Sound currentMusic;
    private Dictionary<string, Sound> soundDictionary;
    private bool _isSoundOn = true;
    private bool _isMusicOn = true;
    public bool isSoundOn{
        get{
            return _isSoundOn;
        }
        set{
            _isSoundOn = value;

            foreach (Sound s in sounds){
                s.source.mute = !_isSoundOn;
            }
        }
    }
    public bool isMusicOn{
        get{
            return _isMusicOn;
        }
        set{
            _isMusicOn = value;
            musicSource.mute = !_isMusicOn;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        soundDictionary = new Dictionary<string, Sound>();

        foreach (Sound s in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            s.source = source;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
            soundDictionary[s.name] = s;
        }
    }

    public void PlaySound(string name)
    {
        if (soundDictionary.TryGetValue(name, out Sound sound))
        {
            sound.source.volume = sound.volume * sfxVolume * masterVolume;
            sound.source.Play();
        }
        else
        {
            Debug.LogWarning($"Sound {name} not found!");
        }
    }

    public void PlayMusic(string name, float fadeTime = 0.25f)
    {
        if (soundDictionary.TryGetValue(name, out Sound sound))
        {
            if (currentMusic != sound)
            {
                StartCoroutine(CrossFadeMusic(sound, fadeTime));
            }
        }
        else
        {
            Debug.LogWarning($"Music {name} not found!");
        }
    }

    private System.Collections.IEnumerator CrossFadeMusic(Sound newMusic, float fadeTime)
    {
        float t = 0;
        float startVolume = musicSource.volume;
        
        // Fade out current music
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            if (musicSource.clip != null)
                musicSource.volume = Mathf.Lerp(startVolume, 0, t / fadeTime);
            yield return null;
        }

        // Set up and start new music
        musicSource.clip = newMusic.clip;
        musicSource.loop = true;
        currentMusic = newMusic;
        musicSource.Play();

        Debug.Log("Playing music: " + newMusic.name);

        // Fade in new music
        t = 0;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0, newMusic.volume * musicVolume * masterVolume, t / fadeTime);
            yield return null;
        }
    }

    public void StopMusic(float fadeTime = 1f)
    {
        StartCoroutine(FadeOutMusic(fadeTime));
    }

    private System.Collections.IEnumerator FadeOutMusic(float fadeTime)
    {
        float startVolume = musicSource.volume;
        float t = 0;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0, t / fadeTime);
            yield return null;
        }

        musicSource.Stop();
        currentMusic = null;
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        UpdateAllVolumes();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        UpdateAllVolumes();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        UpdateAllVolumes();
    }

    private void UpdateAllVolumes()
    {
        if (currentMusic != null)
        {
            musicSource.volume = currentMusic.volume * musicVolume * masterVolume;
        }

        foreach (var sound in sounds)
        {
            if (!sound.source.loop) // Only update SFX volumes
            {
                sound.source.volume = sound.volume * sfxVolume * masterVolume;
            }
        }
    }
}