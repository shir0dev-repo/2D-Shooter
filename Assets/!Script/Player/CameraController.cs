using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _crosshair;
    [SerializeField] private Canvas _canvas;
    private Vector3 _startCameraPos = new Vector3(0, 0, -10);
    private Vector2 _cameraWorldSize;
    private Vector2 _worldToPixelSize;
    private Camera _camera;

    private void OnEnable()
    {
        SceneHandler.OnSceneLoaded += InitializeCamera;
    }

    private void OnDisable()
    {
        SceneHandler.OnSceneLoaded -= InitializeCamera;
    }

    private void Awake()
    {
        _camera = Camera.main;
        _cameraWorldSize.y = _camera.orthographicSize * 2f;
        _cameraWorldSize.x = _cameraWorldSize.y * Screen.width / Screen.height;

        _worldToPixelSize.x = Screen.width / _cameraWorldSize.x;
        _worldToPixelSize.y = Screen.height / _cameraWorldSize.y;
        //Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if (_camera == null)
            return;

        if (MainManager.Instance.SceneHandler.CurrentSceneIndex == 1)
            _camera.transform.position = new Vector3(MainManager.Instance.GameManager.PlayerPosition.x, 3.5f, -10);
        else if (_camera.transform.position != _startCameraPos)
            _camera.transform.position = _startCameraPos;


    }

    private void FixedUpdate()
    {
        if (_camera == null)
            return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -_camera.transform.position.z;
        _crosshair.position = _camera.ScreenToWorldPoint(mousePos);
    }

    public static Vector2 GetOffScreenPosition()
    {
        Camera cam = Camera.main;
        Vector2 offscreenPos = new Vector2(cam.transform.position.x - cam.orthographicSize, cam.transform.position.x + cam.orthographicSize); //X value stores left, Y value stores right.
        return offscreenPos * 2f;
    }

    private void InitializeCamera(int currentSceneIndex)
    {
        _crosshair = transform.GetChild(0);
        _camera = Camera.main;
        _camera.transform.position = _startCameraPos;
        _crosshair.gameObject.SetActive(currentSceneIndex == 1);
        Cursor.visible = currentSceneIndex != 1;
    }

    public IEnumerator CursorExpandCoroutine(float speed = 12f, float apexScale = 7, float animDuration = 0.2f)
    {
        _crosshair.localScale = Vector3.one;

        float timeElapsed = 0;
        float scale;
        while (timeElapsed < animDuration)
        {
            timeElapsed += speed * Time.deltaTime;
            scale = 1 + timeElapsed / animDuration * (apexScale - 1) / apexScale;
            _crosshair.localScale = Vector2.one * scale;

            yield return new WaitForEndOfFrame();
        }

        timeElapsed = animDuration;
        scale = apexScale;

        while (timeElapsed > 0)
        {
            timeElapsed -= Time.deltaTime;
            scale = 1 + (timeElapsed / animDuration * apexScale) / apexScale;
            _crosshair.localScale = Vector2.one * scale;

            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
