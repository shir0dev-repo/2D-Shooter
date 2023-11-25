using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private GameObject _bulletPrefab;

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
        Projectile proj = Instantiate(_bulletPrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();

        proj.SetDirection(target);
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
