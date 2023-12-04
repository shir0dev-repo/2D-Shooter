using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEnemy : Damageable
{
    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
    }

    public override void Die()
    {
        base.Die();

        Debug.Log("Oh no, I'm dying!");

        EnemySpawner.OnEnemyKilled?.Invoke();
        Destroy(gameObject);
    }
}
