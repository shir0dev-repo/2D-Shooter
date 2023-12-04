using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    [SerializeField] private Attack _attack;
    [SerializeField] private Movement _movement;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        if (_attack == null)
            _attack = GetComponentInParent<Attack>();
        if (_movement == null)
            _movement = GetComponentInParent<Movement>();
        if (_animator == null)
            _animator = GetComponent<Animator>();
    }

    public void StartAttack()
    {
        _movement.ToggleMovement(false);
        _animator.SetTrigger("_Attack");
    }

    public void DoAttack() //Called from _AttackSummon animation event.
    {
        _attack.HandleAttack();
        _animator.ResetTrigger("_Attack");
        _movement.ToggleMovement(true);
    }
}
