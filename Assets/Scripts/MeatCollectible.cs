using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatCollectible : MonoBehaviour
{
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        KieferController kieferController = other.GetComponent<KieferController>();
        if (kieferController != null)
        {
            PlayerController playerController = kieferController.player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ConsumeFood();
                playerController.PlaySound(collectedClip);
                Destroy(gameObject);
            }

            EnemyController enemyController = kieferController.player.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.ConsumeFood();
                enemyController.PlaySound(collectedClip);
                Destroy(gameObject);
            }
        }

    }
}
