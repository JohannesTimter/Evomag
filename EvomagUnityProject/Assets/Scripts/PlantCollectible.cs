using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCollectibe : MonoBehaviour
{
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        FiltermundController filtermundController = other.GetComponent<FiltermundController>();
        if (filtermundController != null)
        {
            PlayerController playerController = filtermundController.player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ConsumeFood();
                playerController.PlaySound(collectedClip);
                Destroy(gameObject);
            }

            EnemyController enemyController = filtermundController.player.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.ConsumeFood();
                enemyController.PlaySound(collectedClip);
                Destroy(gameObject);
            }
        }
    }
}
