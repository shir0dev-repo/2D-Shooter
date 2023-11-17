using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] Transform _groundCheck;
    [SerializeField] Vector2 _groundCheckBoxSize;
    [SerializeField] private LayerMask _groundCheckLayer;

    //jumpMultiplier, animcurve
    [Header("Jump Variables")]
    [SerializeField] private float _jumpMultiplier;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private AnimationCurve _jumpCurve;


    private InputAction _jumpAction;

    private void OnEnable()
    {
        //Instance might not exist yet, so check just in case.
        if (PlayerInputHandler.Instance != null)
            PlayerInputHandler.Instance.JumpAction.started += HandleJump;
    }

    private void Start()
    {
        PlayerInputHandler.Instance.JumpAction.started -= HandleJump;
        PlayerInputHandler.Instance.JumpAction.started += HandleJump;
    }


    private void OnDisable()
    {
        PlayerInputHandler.Instance.JumpAction.started -= HandleJump;
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
            StartCoroutine(JumpCoroutine());
    }

    private bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, Vector2.one, 0f, _groundCheckLayer);

        foreach (Collider2D c in colliders)
        {
            if (c.gameObject.layer == LayerMask.NameToLayer("Ground"))
                return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawCube(_groundCheck.position, _groundCheckBoxSize);
    }

    private IEnumerator JumpCoroutine()
    {
        float timeSpentAirborne = 0f;


        while(timeSpentAirborne < _jumpDuration)
        {
            timeSpentAirborne += Time.deltaTime;
            float jumpCompletion = timeSpentAirborne / _jumpDuration; //returns value 0 - 1, over _jumpDuration seconds. ex. jD = 3, returns 0.5f at 1.5 seconds.

            float jumpValue = _jumpCurve.Evaluate(jumpCompletion) * _jumpMultiplier;

            transform.position += Vector3.up * jumpValue * Time.deltaTime;
            
            yield return new WaitForEndOfFrame();

        }

        yield return null; //everythings finished, so you return NOTHING.

    }
}

/*
 
evaluate inside curve 0-1, but extend it based on _jumpduration.
t (0 - jumpduration)

jumpCompletion in the range 0 - 1. we want -1 to 1.

 
*/

//overlap colliders
//adjust and check layers