using UnityEngine;

public class Bramble : Movement
{
    protected override void HandleMovement()
    {
        _rigidbody.velocity = Vector2.left * _maxSpeed;
    }

    protected override void Awake()
    {
        base.Awake();

        _rigidbody.gravityScale = 0;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
