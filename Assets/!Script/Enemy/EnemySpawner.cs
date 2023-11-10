using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _enemyMoveSpeed = 8f;
    private bool hasSpawned = false;

    private void Awake()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        if (!hasSpawned)
        {
            GameObject enemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(20, 60), 0, 0), Quaternion.identity);
            hasSpawned = true;
            //GameObject enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        }

    }
    private void FixedUpdate()
    {
        Vector3 moveDirection = _enemyMoveSpeed * Time.fixedDeltaTime * new Vector3(-1f, 0, 0).normalized;
        _rigidbody.AddForce(moveDirection, ForceMode2D.Impulse);

    }
}
