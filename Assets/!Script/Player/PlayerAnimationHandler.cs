using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    Animator _playerAnimator;

    private const string _LAND_TRIGGER = "_OnPlayerLanded";
    private const string _JUMP_TRIGGER = "_OnPlayerJump";
    private const string _VELOCITY = "_Velocity";
    private const string _DEATH = "_Death";

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerJump.OnPlayerLanded += LandAnimation;
        PlayerJump.OnPlayerJump += JumpAnimation;
        PlayerJump.OnVerticalVelocityChanged += SetAnimatorVelocity;
    }

    private void OnDisable()
    {
        PlayerJump.OnPlayerLanded -= LandAnimation;
        PlayerJump.OnPlayerJump -= JumpAnimation;
        PlayerJump.OnVerticalVelocityChanged -= SetAnimatorVelocity;
    }

    public void SetAnimatorVelocity(float verticalVelocity)
    {
        _playerAnimator.SetFloat(_VELOCITY, verticalVelocity);
    }

    private void JumpAnimation() => _playerAnimator.SetTrigger(_JUMP_TRIGGER);
    private void LandAnimation() => _playerAnimator.SetTrigger(_LAND_TRIGGER);
    public void DeathAnimation(Action callback)
    {
        _playerAnimator.SetTrigger(_DEATH);
        StartCoroutine(DeathAnimationCoroutine(callback));
    }


    private IEnumerator DeathAnimationCoroutine(Action callback) //Coroutines (IEnumerators) run separately from main game loop.
    {

        float animationDuration = _playerAnimator.GetCurrentAnimatorStateInfo(0).length;
        
        yield return new WaitForSeconds(animationDuration); //Yield essentially means ignore everything after until timer is up

        callback();
    }
}
