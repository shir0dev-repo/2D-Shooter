using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSummon : Attack
{
    [SerializeField] GameObject _summonedPrefab;

    private int _maxMinions = 3;

    private List<GameObject> _summonedMinions;

    [SerializeField] private EnemyAnimationHandler _enemyAnimationHandler;

    public Action<bool> OnAttack;

    private void Awake()
    {
        _summonedMinions = new List<GameObject>();

        if (_enemyAnimationHandler == null)
            _enemyAnimationHandler = GetComponentInChildren<EnemyAnimationHandler>();
    }

    protected override void Update()
    {
        base.Update();

        if (_attackReady && _summonedMinions.Count < _maxMinions)
            InitAttack();
    }

    public override void HandleAttack(GameObject target = null)
    {
        //halt movement somehow
        //play animation
        GameObject minion = Instantiate(_summonedPrefab, GetSummonPosition(), Quaternion.identity);
        _summonedMinions.Add(minion);

        base.HandleAttack(); //reset attack timer

        OnAttack?.Invoke(false);
    }

    private void InitAttack()
    {
        _enemyAnimationHandler.StartAttack();
        OnAttack?.Invoke(true);
    }

    private Vector2 GetSummonPosition() => transform.position + (-transform.right * 2f);
}