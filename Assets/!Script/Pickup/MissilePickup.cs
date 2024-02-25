using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePickup : Pickup
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out PlayerAttack pAtk)) return;
        else if (pAtk.HasMissile) return;

        pAtk.AddMissile();
        Destroy(gameObject);
    }
}