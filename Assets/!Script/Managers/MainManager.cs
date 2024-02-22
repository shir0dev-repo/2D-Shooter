using UnityEngine;

public class MainManager : PersistentSingleton<MainManager>
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private SceneHandler _sceneHandler;

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

    protected override void Awake()
    {
        base.Awake();

        _gameManager = gameObject.GetComponentInChildren<GameManager>();
        _audioManager = gameObject.GetComponentInChildren<AudioManager>();
        _uiManager = gameObject.GetComponentInChildren<UIManager>();
    }

    public void RestartGame()
    {
        _sceneHandler.LoadScene(SceneHandler.CurrentSceneIndex);
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
