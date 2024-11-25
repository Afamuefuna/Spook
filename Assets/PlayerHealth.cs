using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerHealth : MonoBehaviour
{
    public float invincibilityDuration = 2f;
    public float blinkInterval = 0.2f;

    private SpriteRenderer spriteRenderer;
    private bool isInvincible = false;
    private Color originalColor;
    public float fadeInterval = 0.1f;
    public float minAlpha = 0.2f;

    private void Start()
    {
        GameManager.Instance.currentHealth = GameManager.Instance.maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trap"))
        {
            TakeDamage(1);
        }

        if(other.CompareTag("DeathZone")){
            PlayerController playerController = FindAnyObjectByType<PlayerController>();
            playerController.transform.position = playerController.restartPoint;

            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        AudioManager.Instance.PlaySound(SoundEffect.HURT);
        GameManager.Instance.currentHealth -= damage;
        GameManager.Instance.UpdateHealthDisplay();

        Debug.Log($"Player took {damage} damage. Current health: {GameManager.Instance.currentHealth}");

        if (GameManager.Instance.currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Invincibility());
        }
    }

    private void Die()
    {
        Debug.Log("Player died");
        // Add your game over logic here
    }

    private IEnumerator Invincibility()
    {
        PlayerController playerController = FindAnyObjectByType<PlayerController>();
        playerController.movementParticle.gameObject.SetActive(false);
        playerController.groundParticle.gameObject.SetActive(false);

        isInvincible = true;
        float endTime = Time.time + invincibilityDuration;

        while (Time.time < endTime)
        {
            // Fade out
            yield return StartCoroutine(FadeSprite(255, minAlpha));

            yield return new WaitForSeconds(0.25f);
            
            // Fade in
            yield return StartCoroutine(FadeSprite(minAlpha, 255));
        }

        // Ensure the sprite is fully opaque at the end
        SetAlpha(255);
        isInvincible = false;

        playerController.movementParticle.gameObject.SetActive(true);
        playerController.groundParticle.gameObject.SetActive(true);
    }

    private IEnumerator FadeSprite(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < fadeInterval)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeInterval);
            SetAlpha(newAlpha);
            yield return null;
        }

        SetAlpha(endAlpha);
    }

    private void SetAlpha(float alpha)
    {
        Color newColor = spriteRenderer.color;
        newColor.a = alpha;
        spriteRenderer.color = newColor;
    }
}