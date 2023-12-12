using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEnemy : Damageable
{
    [SerializeField] private bool _spawnNewOnDeath = false;
    [SerializeField] int _pointsWorth;

    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
    }

    public override void Die()
    {
        base.Die();

        Debug.Log("Oh no, I'm dying!");

        if (_spawnNewOnDeath)
            EnemySpawner.OnEnemyKilled?.Invoke();

        GameManager.Instance.OnScoreIncremented?.Invoke(_pointsWorth);

        Destroy(gameObject);
    }

    public override void Restart()
    {
        base.Restart();
        Destroy(gameObject);
    }
}
