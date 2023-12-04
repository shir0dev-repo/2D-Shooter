using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;

    private Vector3 _lastStoredPosition = Vector3.zero;

    //A Static "Instance" (also known as a Singleton Pattern) is a globally accessible reference to THIS SPECIFIC INSTANCE of the script.
    //Typically, it is common (default even) practice to only have ONE instance of the Singleton's type, and destroy all other instances that may be created later.
    public static GameManager Instance;

    public Action OnPlayerDeath { get; set; }

    public Vector3 PlayerPosition
    {
        get
        {
            if (_player != null)
            {
                _lastStoredPosition = _player.transform.position;
                return _player.transform.position;
            }

            else
            {
                return _lastStoredPosition;
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
    }

    private void DisablePlayer()
    {
        _player.GetComponent<PlayerInputHandler>().enabled = false;
        _player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }
}
