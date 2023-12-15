using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : Singleton<EnemySpawner>, IRestartable
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private static List<GameObject> _spawnedEnemies = new();
    public static Action OnEnemyKilled { get; private set; }

    private void OnEnable()
    {
        OnEnemyKilled += SpawnEnemy;
        (this as IRestartable).Subscribe();
    }

    private void Start()
    {
        SpawnEnemy();
        StartCoroutine(SpawnObstacleCoroutine());
    }

    private void OnDisable()
    {
        OnEnemyKilled -= SpawnEnemy;
        (this as IRestartable).Unsubscribe();
        StopAllCoroutines();
    }

    private Vector3 GetSpawnPosition()
    {
        float xPos = GameManager.Instance.PlayerPosition.x;
        xPos += Camera.main.orthographicSize + Random.Range(10, 20f);
        Vector3 spawnPos;

        try //"if" code here doesn't work/throws error, "catch" the error (as an else statement)
        {
            RaycastHit2D groundSpawnCast = Physics2D.Raycast(new(xPos, 15f), Vector2.down, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ground"));
            spawnPos = groundSpawnCast.point;
        }
        catch (Exception e) //basically an else with error "catching"
        {
            Debug.LogErrorFormat("Raycast hit nothing!" + e);
            spawnPos = new Vector3(xPos, 0, 0);
        }

        return spawnPos;
    }
    public void RemoveEnemy(GameObject enemy)
    {
        _spawnedEnemies.Remove(enemy);
    }
    void SpawnEnemy()
    {
        if (!GameManager.Instance.PlayerAlive || _spawnedEnemies.Count > 0) return;

        Vector3 spawnPos = GetSpawnPosition();

        _spawnedEnemies.Add(Instantiate(_enemyPrefab, spawnPos, Quaternion.identity));
    }

    public GameObject SpawnEnemy(GameObject enemy, float xPosition)
    {
        if (!GameManager.Instance.PlayerAlive) return null;
        Vector3 spawnPos;
        
        try //"if" code here doesn't work/throws error, "catch" the error (as an else statement)
        {
            RaycastHit2D groundSpawnCast = Physics2D.Raycast(new(xPosition, 15f), Vector2.down, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ground"));
            spawnPos = groundSpawnCast.point;
        }
        catch (Exception e) //basically an else with error "catching"
        {
            Debug.LogErrorFormat("Raycast hit nothing!" + e);
            spawnPos = new Vector3(xPosition, 0, 0);
        }

        return Instantiate(enemy, spawnPos, Quaternion.identity);
    }

    [ContextMenu("Force New Spawn")]
    public void ForceInvoke()
    {
        OnEnemyKilled?.Invoke();
    }

    public void Restart()
    {
        _spawnedEnemies.Clear();
        SpawnEnemy();
        StopAllCoroutines();
        StartCoroutine(SpawnObstacleCoroutine());
    }

    IEnumerator SpawnObstacleCoroutine()
    {
        while (GameManager.Instance.PlayerAlive)
        {
            int waitTime = Random.Range(5, 12);
            yield return new WaitForSeconds(waitTime);

            if (!GameManager.Instance.PlayerAlive) break;

            SpawnEnemy(_obstaclePrefab, GetSpawnPosition().x);
        }
    }
}



/*
 
Actions are a LIST of functions ADDED to a single event.
when saying myEvent += myFunction, you're ADDING the function to the list of events to invoke.
when saying myEvent -= myFunction, youre REMOVING the function from the list of events to invoke.

*/