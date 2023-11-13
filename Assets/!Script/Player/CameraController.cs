using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private void LateUpdate()
    {
        transform.position = new Vector3(_playerTransform.position.x, 7, -10);
    }
}
