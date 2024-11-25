using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class Coin : MonoBehaviour
{
    public float floatDistance = 0.5f;
    public float floatDuration = 1f;
    Sequence floatSequence;

    private void Start()
    {
        // Get the initial position of the object
        Vector3 startPosition = transform.position;

        // Create a sequence of movements
        floatSequence = DOTween.Sequence();

        // Move up
        floatSequence.Append(transform.DOMoveY(startPosition.y + floatDistance, floatDuration / 2)
            .SetEase(Ease.InOutSine));

        // Move down
        floatSequence.Append(transform.DOMoveY(startPosition.y, floatDuration / 2)
            .SetEase(Ease.InOutSine));

        // Set the sequence to loop indefinitely
        floatSequence.SetLoops(-1, LoopType.Restart);

    }

   public float moveDuration = 0.5f;
    public RectTransform coinUIImage; // Reference to the UI Image RectTransform

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySound(SoundEffect.COIN_PICKUP);
            StartCoroutine(MoveToUIPosition());
        }
    }

    private IEnumerator MoveToUIPosition()
    {
        coinUIImage = FindAnyObjectByType<CoinIcon>().gameObject.GetComponent<RectTransform>();

        // Disable the collider to prevent multiple collisions
        GetComponent<Collider2D>().enabled = false;

        // Convert the UI position to world space
        Vector3 startPosition = transform.position;
        Vector3 endPosition = Camera.main.ScreenToWorldPoint(coinUIImage.position);
        endPosition.z = 0; // Set z to 0 for 2D

        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            endPosition = Camera.main.ScreenToWorldPoint(coinUIImage.position);
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        // Ensure the coin reaches the exact end position
        transform.position = endPosition;
        GameManager.Instance.CollectCoin();

        // Optionally destroy the coin after reaching the UI
        Destroy(gameObject);
    }
}