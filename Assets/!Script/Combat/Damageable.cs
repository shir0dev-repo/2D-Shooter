using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Damageable : MonoBehaviour, IDamageable, IRestartable
{
    [SerializeField] protected int _maxHealth;
    protected Rigidbody2D _rigidbody;
    protected int _currentHealth;
    protected bool _isInvulnerable;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;
    public bool IsInvulnerable => _isInvulnerable;

    public Action<int> OnHealthChanged;


    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentHealth = _maxHealth;
    }
    private void OnEnable()
    {
        (this as IRestartable).Subscribe(); //Up-casting to inherited interface IRestartable.
    }

    private void OnDisable()
    {
        (this as IRestartable).Unsubscribe();
    }

    protected virtual void Start()
    {
        OnHealthChanged?.Invoke(_currentHealth);
    }

    public virtual void TakeDamage(int damageAmount)
    {
        if (_isInvulnerable) return;

        _currentHealth -= damageAmount;
        OnHealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        if (_currentHealth > 0)
        {
            Debug.LogWarning($"{gameObject.name}'s health is not low enough to die!");
            return;
        }
    }

    public virtual void Restart()
    {
        _currentHealth = _maxHealth;
    }
}
