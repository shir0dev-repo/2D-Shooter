using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] protected float _attackCooldown = 5f;
    [SerializeField] protected float _attackCooldownRemaining = 5f;
    [SerializeField] protected bool _attackReady = false;
    [SerializeField] protected AttackType _attackType;

    public virtual void HandleAttack(GameObject target = null)
    {
        _attackReady = false;
        _attackCooldownRemaining = _attackCooldown;
    }

    protected virtual void Update()
    {
        if (!_attackReady)
        {
            _attackCooldownRemaining -= Time.deltaTime;
            if ( _attackCooldownRemaining <= 0)
                _attackReady = true;
        }
    }
}

[Flags]
public enum AttackType
{
    Stalk,
    Summon,
    Chase,
    Shoot
}