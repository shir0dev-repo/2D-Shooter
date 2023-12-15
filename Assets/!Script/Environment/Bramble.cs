using UnityEngine;

public class Bramble : Movement, IRestartable
{


    protected override void Awake()
    {
        base.Awake();

        _rigidbody.gravityScale = 0;
    }

    private void OnEnable()
    {
        (this as IRestartable).Subscribe();
    }

    private void OnDisable()
    {
        (this as IRestartable).Unsubscribe();
    }

    protected override void HandleMovement()
    {
        _rigidbody.velocity = Vector2.left * _maxSpeed;
    }

    public void Restart()
    {
        Destroy(gameObject);
    }
}
