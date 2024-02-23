using UnityEngine;

public class MainManager : PersistentSingleton<MainManager>
{
    private static bool _hasBeenInitialized = false;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private SceneHandler _sceneHandler;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private EnemySpawner _enemySpawner;

    public GameManager GameManager
    {
        get { return _gameManager; }
    }
    public AudioManager AudioManager
    {
        get
        {
            return _audioManager;
        }
    }
    public UIManager UIManager
    {
        get
        {
            return _uiManager;
        }
    }
    public SceneHandler SceneHandler
    {
        get
        {
            return _sceneHandler;
        }
    }

    public CameraController CameraController
    {
        get { return _cameraController; }
    }

    public EnemySpawner EnemySpawner
    {
        get { return _enemySpawner; }
    }

    protected override void Awake()
    {
        base.Awake();
        Initialize(0);

        SceneHandler.OnSceneLoaded += Initialize;
    }

    private void Initialize(int _)
    {
        _gameManager = gameObject.GetComponentInChildren<GameManager>();
        _audioManager = gameObject.GetComponentInChildren<AudioManager>();
        _uiManager = gameObject.GetComponentInChildren<UIManager>();
        _sceneHandler = gameObject.GetComponentInChildren<SceneHandler>();
        _cameraController = gameObject.GetComponentInChildren<CameraController>();

        Debug.Log(gameObject.GetInstanceID());
    }

    public void RestartGame()
    {
        GameManager.OnGameRestart?.Invoke();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
