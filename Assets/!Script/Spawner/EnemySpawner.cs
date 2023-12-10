using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private List<GameObject> _enemyDataList;

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
        float xPos = Camera.main.transform.position.x;
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


        GameObject enemy = _enemyDataList[Random.Range(0, _enemyDataList.Count)];

        Instantiate(enemy, spawnPos, Quaternion.identity);
    }

    public GameObject SpawnEnemy(GameObject enemy, float xPosition)
    {
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
}

/*
 
Actions are a LIST of functions ADDED to a single event.
when saying myEvent += myFunction, you're ADDING the function to the list of events to invoke.
when saying myEvent -= myFunction, youre REMOVING the function from the list of events to invoke.

*/