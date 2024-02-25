using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStartManager : Singleton<GameStartManager>
{
    [SerializeField] private GameObject _mainManagerPrefab;
    [SerializeField] private ParticleSystem _notePS;
    protected override void Awake()
    {
        base.Awake();
        if (MainManager.Instance == null)
            Instantiate(_mainManagerPrefab);

        Destroy(gameObject);
    }
}