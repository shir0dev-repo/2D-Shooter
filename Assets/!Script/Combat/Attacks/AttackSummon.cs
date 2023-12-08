using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSummon : Attack
{
    [SerializeField] GameObject _summonedPrefab;

    [SerializeField] private int _maxMinions = 3;

    private List<GameObject> _summonedMinions;

    [SerializeField] private EnemyAnimationHandler _enemyAnimationHandler;
    [SerializeField] private Vector2 _summonPoint;

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
        //remove null minions (they died, but still exist as null references)
        _summonedMinions.RemoveAll(minion => minion == null);

        GameObject minion = Instantiate(_summonedPrefab, GetSummonPosition(out Quaternion rotation), rotation);
        _summonedMinions.Add(minion);

        base.HandleAttack(); //reset attack timer
    }

    private void InitAttack()
    {
        _enemyAnimationHandler.StartAttack();
    }

    private Vector2 GetSummonPosition(out Quaternion rotation)
    {
        float direction = transform.TransformDirection(-Vector3.right).x * _summonPoint.x;
        float yRotation = direction < transform.position.x ? 180 : 0;

        rotation = Quaternion.Euler(0, yRotation, 0);
        Vector3 position = transform.position + new Vector3(direction, _summonPoint.y, 0);
        return position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(GetSummonPosition(out _), 0.5f);
    }
}