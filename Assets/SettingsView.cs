using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : UIView
{
    [SerializeField] Button closeButton;
    [SerializeField] Button soundBtn, musicBtn;
    [SerializeField] TMP_Text soundTxt, musicTxt;

    public override void Show()
    {
        base.Show();

        if(AudioManager.Instance.isSoundOn){
            soundTxt.text = "On";
        }else{
            soundTxt.text = "Off";
        }

        if(AudioManager.Instance.isMusicOn){
            musicTxt.text = "On";
        }else{
            musicTxt.text = "Off";
        }
    }

    public override void Hide()
    {
        base.Hide();
    }

    void Start(){
        uIManager = FindAnyObjectByType<UIManager>();
        closeButton.onClick.AddListener(()=>{
            uIManager.Transition(uIManager.menuView);
        });

        soundBtn.onClick.AddListener(()=> 
        {
            AudioManager.Instance.isSoundOn = !AudioManager.Instance.isSoundOn;
            if(AudioManager.Instance.isSoundOn){
                soundTxt.text = "On";
            }else{
                soundTxt.text = "Off";
            }
        });

        musicBtn.onClick.AddListener(()=>{
            AudioManager.Instance.isMusicOn = !AudioManager.Instance.isMusicOn;
            if(AudioManager.Instance.isMusicOn){
                musicTxt.text = "On";
            }else{
                musicTxt.text = "Off";
            }
        });
    }
}
