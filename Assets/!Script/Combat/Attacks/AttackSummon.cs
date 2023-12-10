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

    public override void HandleAttack() //This better count as a projectile ;3
    {
        //remove null minions (they died, but still exist as null references)
        _summonedMinions.RemoveAll(minion => minion == null);

        GameObject minion = EnemySpawner.Instance.SpawnEnemy(_summonedPrefab, GetSummonPositionX());
        _summonedMinions.Add(minion);

        base.HandleAttack(); //reset attack timer
    }

    private void InitAttack()
    {
        _enemyAnimationHandler.StartAttack();
    }

    private float GetSummonPositionX()
    {
        float direction = transform.TransformDirection(-Vector3.right).x * _summonPoint.x;
        float posX = transform.position.x + direction;
        
        return posX;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(new Vector3(GetSummonPositionX() + transform.position.x, transform.position.y, 0f ), transform.localScale);
    }
}