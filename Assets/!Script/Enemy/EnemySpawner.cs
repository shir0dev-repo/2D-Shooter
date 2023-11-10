using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    private void Start()
    {    
            GameObject enemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(20, 60), 0, 0), Quaternion.identity);
            //GameObject enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
    }
}
