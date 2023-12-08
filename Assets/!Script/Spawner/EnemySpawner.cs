using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private List<EnemyData> _enemyDataList;

    public static Action OnEnemyKilled { get; private set; }

    private void OnEnable()
    {
        OnEnemyKilled += SpawnEnemy;
    }

    private void Start()
    {
        SpawnEnemy();
    }

    private void OnDisable()
    {
        OnEnemyKilled -= SpawnEnemy;
    }

    void SpawnEnemy()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 offScreenPosition = new Vector3(cameraPos.x, -3.5f, 0);

        offScreenPosition.x += Camera.main.orthographicSize + Random.Range(10, 20f);
        Instantiate(_enemyPrefab, offScreenPosition, Quaternion.identity);
    }
}

/*
 
Actions are a LIST of functions ADDED to a single event.
when saying myEvent += myFunction, you're ADDING the function to the list of events to invoke.
when saying myEvent -= myFunction, youre REMOVING the function from the list of events to invoke.

*/