using System.Collections.Generic;
using UnityEngine;

public class DamageableEnemy : Damageable
{
    [SerializeField] private bool _spawnNewOnDeath = false;
    [SerializeField] int _pointsWorth;
    [SerializeField] private List<GameObject> _pickups;
    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
    }

    public override void Die()
    {
        //base.Die();

        MainManager.Instance.EnemySpawner.RemoveEnemy(gameObject);

        if (_spawnNewOnDeath)
            EnemySpawner.OnEnemyKilled?.Invoke();

        MainManager.Instance.GameManager.OnScoreIncremented?.Invoke(_pointsWorth);
        TryDropPickup();
        Destroy(gameObject);
    }

    private void TryDropPickup()
    {
        foreach (GameObject go in _pickups)
        {
            if (!go.TryGetComponent(out Pickup pickup))
                continue;
            if (Random.value > pickup.SpawnChance)
                continue;

            Instantiate(go, transform.position, Quaternion.identity);
            break;
        }
    }

    public override void Restart()
    {
        base.Restart();
        Destroy(gameObject);
    }
}
