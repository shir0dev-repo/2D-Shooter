using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private float _attackCooldown = 5f;
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private EnemyMovement _movement;
    [SerializeField] private Animator _animator;


    private float _cooldownRemaining = 1f;

    private bool _canAttack = true;

    private void Start()
    {
        GameManager.Instance.OnPlayerDeath += DisableAttack;
    }

    private void DisableAttack()
    {
        _canAttack = false;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerDeath -= DisableAttack;
    }

    public void Shoot(Vector3 target)
    {
        _animator.SetTrigger("_Attack");

        Vector3 spawnPosition = (transform.position - target).normalized * 1.5f;
        Instantiate(_bulletPrefab, spawnPosition, Quaternion.identity);
    }

    private void Update()
    {
        if (!_canAttack) return;

        _cooldownRemaining -= Time.deltaTime;

        if (_cooldownRemaining <= 0f)
        {
            Shoot(GameManager.Instance.PlayerPosition);
            _cooldownRemaining = _attackCooldown;
        }
    }
}
