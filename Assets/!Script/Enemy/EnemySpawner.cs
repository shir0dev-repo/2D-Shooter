using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    bool targetHit = false;

    private void FixedUpdate()
    {
        if (targetHit == true)
        {
            Destroy(_enemyPrefab);
        }
    }
    private void Start()
    {    
            GameObject enemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-20, -30), 0, 0), Quaternion.identity);
            //GameObject enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            targetHit = true;
        }
    }
}
