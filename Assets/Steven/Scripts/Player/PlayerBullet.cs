using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyAI enemyComponent))
        {
            enemyComponent.TakeDamage(1);
        }
        if (collision.gameObject.TryGetComponent(out EnemyAILVL2 enemyComponent2))
        {
            enemyComponent2.TakeDamage(1);
        }
        if (collision.gameObject.TryGetComponent(out enemyAILVL3 enemycomponent3))
        {
            enemycomponent3.TakeDamage(1);
        }

    }
}
