using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    public static Action OnEnemyKilled { get; private set; }

    private void OnEnable()
    {
        OnEnemyKilled += SpawnEnemy;
    }

    private void Start()
    {
        OnEnemyKilled?.Invoke(); //the ? is the exact same as using a null check (OnEnemyKilled != null)

        //if the action HAS listeners
        /*
         if (OnEnemyKilled != null)
         {
            OnEnemyKilled.Invoke();
         }
        */
    }

    private void OnDisable()
    {
        OnEnemyKilled -= SpawnEnemy;
    }

    void SpawnEnemy()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 offScreenPosition = new Vector3(cameraPos.x, 0, 0);

        offScreenPosition.x += Camera.main.orthographicSize + Random.Range(10, 20f);
        Instantiate(_enemyPrefab, offScreenPosition, Quaternion.identity);
    }
}

/*
 
Actions are a LIST of functions ADDED to a single event.
when saying myEvent += myFunction, you're ADDING the function to the list of events to invoke.
when saying myEvent -= myFunction, youre REMOVING the function from the list of events to invoke.

*/