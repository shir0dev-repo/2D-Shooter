using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] RectTransform _cursorSprite;

    Vector2 _cameraWorldSize;
    Vector2 _worldToPixelSize;

    Camera _camera;

    protected override void Awake()
    {
        base.Awake();

        _camera = GetComponent<Camera>();
        _cameraWorldSize.y = _camera.orthographicSize * 2f;
        _cameraWorldSize.x = _cameraWorldSize.y * Screen.width / Screen.height;

        _worldToPixelSize.x = Screen.width / _cameraWorldSize.x;
        _worldToPixelSize.y = Screen.height / _cameraWorldSize.y;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    private void Update()
    {
        transform.position = new Vector3(GameManager.Instance.PlayerPosition.x, 3.5f, -10);
    }

    private void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        _cursorSprite.position = PixelToWorldPoint(mousePos);
    }

    public Vector2 GetOffScreenPosition()
    {
        Vector2 offscreenPos = new Vector2(transform.position.x - _camera.orthographicSize, transform.position.x + _camera.orthographicSize); //X value stores left, Y value stores right.
        return offscreenPos * 2f;
    }

    private Vector2 PixelToWorldPoint(Vector2 pixelPosition)
    {
        Vector2 position;

        position.x = ((pixelPosition.x / _worldToPixelSize.x) - (_cameraWorldSize.x / 2f)) + transform.position.x;
        position.y = ((pixelPosition.y / _worldToPixelSize.y) - (_cameraWorldSize.y / 2f)) + transform.position.y;

        return position;
    }

    public IEnumerator CursorExpandCoroutine(float speed = 12f, float apexScale = 7, float animDuration = 0.2f)
    {
        _cursorSprite.localScale = Vector3.one;

        float timeElapsed = 0;
        float scale;
        while (timeElapsed < animDuration)
        {
            timeElapsed += speed * Time.deltaTime;
            scale = 1 + timeElapsed / animDuration * (apexScale - 1) / apexScale;
            _cursorSprite.localScale = Vector2.one * scale;

            yield return new WaitForEndOfFrame();
        }

        timeElapsed = animDuration;
        scale = apexScale;

        while (timeElapsed > 0)
        {
            timeElapsed -= Time.deltaTime;
            scale = 1 + (timeElapsed / animDuration * apexScale) / apexScale;
            _cursorSprite.localScale = Vector2.one * scale;

            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
