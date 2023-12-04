using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour, IDamageable
{
    [SerializeField] protected int _maxHealth;
    protected int _currentHealth;

    public int MaxHealth { get { return _maxHealth; } }
    public int CurrentHealth { get { return _currentHealth; } }

    protected virtual void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public virtual void TakeDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        if (_currentHealth > 0)
        {
            Debug.LogWarning("Health is not low enough to die!");
            return;
        }
    }
}
