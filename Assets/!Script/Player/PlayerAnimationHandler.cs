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

    private void JumpAnimation()
    {
        Debug.Log("jump");
        _playerAnimator.SetTrigger(_JUMP_TRIGGER);
//        _playerAnimator.ResetTrigger(_JUMP_TRIGGER);
    }

    private void LandAnimation()
    {
        Debug.Log("land");
        _playerAnimator.SetTrigger(_LAND_TRIGGER);
  //      _playerAnimator.ResetTrigger(_LAND_TRIGGER);
    }
}
