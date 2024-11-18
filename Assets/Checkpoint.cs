using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    PlayerController player;
    [SerializeField] bool isFinishLine;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !this.CompareTag("DeathZone"))
        {
            player = other.GetComponent<PlayerController>();
            player.restartPoint = player.transform.position;

            if(isFinishLine)
            {
                GameManager.Instance.CompleteLevel();
            }
        }
    }
}
