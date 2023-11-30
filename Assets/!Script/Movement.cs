using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Abstract classes are MADE to be derived from. Abstract MonoBehaviour's can NOT be put on a GameObject inside the Inspector.
//In terms of etymology, abstract essentially means a base concept. With abstract classes, MonoBehaviours can be created LATER, that share the same fields as 
//this base class, but put a "twist" on it. For example, both enemy and player movement scripts, will move around, but the enemy will follow the player (generally),
//but the player is moving through input.
public abstract class Movement : MonoBehaviour
{
    //Protected is private with the ability to pass down to children.

    //Movement._moveSpeed;
    [Header("Base")]
    [SerializeField, Min(0)] protected float _moveSpeed;
    [SerializeField, Min(0)] protected float _maxSpeed;
    [SerializeField] protected Rigidbody2D _rigidbody;
    protected bool _canMove;

    protected abstract void HandleMovement(); //Idea

    protected virtual void Awake()
    {
        //Base Implementation
        if(_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        HandleMovement();
    }
}

/*
 
public = Starbucks
protected = family home
private = your mind
 
 */