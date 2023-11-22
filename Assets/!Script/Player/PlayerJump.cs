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
    [SerializeField] private float _jumpDuration;

    [SerializeField] private AnimationCurve _animCurve;
    float velocityLastFrame = 0;
    Vector2 positionLastFrame = Vector2.zero;

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

    private void FixedUpdate()
    {
        float verticalDisplacement = transform.position.y - positionLastFrame.y;


    }

    private void OnDisable()
    {
        PlayerInputHandler.Instance.JumpAction.started -= HandleJump;
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if (!IsGrounded()) return;
            
        /*NEED:
          

            - Gravity (non-linear)
            - time spent airborne variable
            - maybe curve will work actually?
            - velocityLastFrame
            - velocityThisFrame
            - time (in this case, evaluationPercent)
            - displacement = initialVelocity * time + (1/2 * acceleration * time^2)
            - displacement = (velocityLastFrame * evaluationPercent + 0.5f * curveMultiplier * evaluationPercent * evaluationPercent) * jumpForce;
            - finalVelocity = 

            - how to customize this ^^^ ?

        float evaluationPercent = Mathf.Clamp01(timeSpentAirborne / maxJumpDuration);

        float curveMultiplier = 1 - _animCurve.Evaluate(evaluationPercent);
        player.transform.position.y -= maxPlayerGravity * curveMultiplier;
         
        */

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

}