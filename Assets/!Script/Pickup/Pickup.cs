using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Pickup : MonoBehaviour
{
    [SerializeField, Range(0, 1)] protected float _spawnChance = 0.6f;
    public float SpawnChance => _spawnChance;

    protected abstract void OnTriggerEnter2D(Collider2D other);
}
