using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.ChangeHealth(-1);
        }

        EnemyController enemyController = other.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.ChangeHealth(-1);
        }
    }
}
