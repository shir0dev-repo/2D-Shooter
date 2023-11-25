using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Update()
    {
        transform.position = new Vector3(GameManager.Instance.PlayerPosition.x, 3.5f, -10);
    }
}
