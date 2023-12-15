using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectPrefab;
    public static Action OnObstacleDestroy {  get; private set; }
    private void OnEnable()
    {
        OnObstacleDestroy += SpawnObstacle;
    }
    void Start()
    {
        OnObstacleDestroy?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        OnObstacleDestroy -= SpawnObstacle;
    }
    void SpawnObstacle()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 offScreenPosition = new Vector3(cameraPos.x, -9.3f, 0);

        offScreenPosition.x = Camera.main.orthographicSize + Random.Range(10, 20f);
        Instantiate(_objectPrefab, offScreenPosition, Quaternion.identity);
    }
}
