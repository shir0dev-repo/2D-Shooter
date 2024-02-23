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

        MainManager.Instance.EnemySpawner.RemoveEnemy(gameObject);

        if (_spawnNewOnDeath)
            EnemySpawner.OnEnemyKilled?.Invoke();

        MainManager.Instance.GameManager.OnScoreIncremented?.Invoke(_pointsWorth);
        Destroy(gameObject);
    }

    public override void Restart()
    {
        base.Restart();
        Destroy(gameObject);
    }
}
