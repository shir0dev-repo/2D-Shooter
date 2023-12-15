using System;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] Transform _spawnPoint;
    [Space]
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _highScoreText;
    [SerializeField] GameObject _restartUIPanel;
    [SerializeField] GameObject _restartBtn;

    public Action<int> OnScoreIncremented;
    public static Action OnPlayerDeath { get; set; }
    public static Action OnGameRestart { get; set; }
    public Vector3 PlayerPosition => GetPlayerPosition();
    public bool PlayerAlive => _playerAlive;

    private bool _playerAlive = false;
    private int _currentScore = 0;
    private int _highScore = 0;
    private const string SCORE_PREFIX = "Score: ";
    private const string HIGH_SCORE_PREFIX = "Best: ";
    private static Vector2Int scorePosOnDeath = new Vector2Int(-150, 125);
    private static Vector2Int defaultScorePos = new Vector2Int(-512, 365);

    Vector3 _lastStoredPosition = Vector3.zero;
    PlayerMovement _playerMovement;
    protected override void Awake()
    {
        base.Awake();

        UpdateScoreText();

        SpawnPlayer();
        GetPlayerPosition();
    }

    private void OnEnable()
    {
        OnScoreIncremented += IncrementScore;
        OnPlayerDeath += KillPlayer;

        _playerAlive = true;
    }

    private void OnDisable()
    {
        OnScoreIncremented -= IncrementScore;

        OnPlayerDeath -= KillPlayer;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void KillPlayer()
    {
        _playerAlive = false;
        ToggleUI();
    }

    public void RestartGame()
    {
        SpawnPlayer();
        OnGameRestart?.Invoke();
        ResetScore();
    }

    void ResetScore()
    {
        _currentScore = 0;
        UpdateScoreText();
        _scoreText.rectTransform.anchoredPosition = defaultScorePos;
    }
    void UpdateScoreText() => _scoreText.text = SCORE_PREFIX + _currentScore.ToString();
    void UpdateHighScoreText() => _highScoreText.text = HIGH_SCORE_PREFIX + _highScore.ToString();
    void IncrementScore(int score)
    {
        _currentScore += score;

        UpdateScoreText();
    }
    void FindBestScore()
    {
        _highScore = (_currentScore > _highScore) ? _currentScore : _highScore;
    }

    private Vector2 GetPlayerPosition()
    {
        if (_playerMovement != null)
        {
            _ = _lastStoredPosition = _playerMovement.transform.position;

            _playerAlive = true;
            return _playerMovement.transform.position;
        }

        else
        {
            _playerAlive = false;
            return _lastStoredPosition;
        }
    }

    void SpawnPlayer()
    {
        GameObject player = Instantiate(_playerPrefab, _spawnPoint.position, Quaternion.identity);
        _playerMovement = player.GetComponent<PlayerMovement>();
        _ = GetPlayerPosition();
    }

    public void ToggleUI()
    {
        bool panelActive = _restartUIPanel.activeSelf;
        _restartBtn.SetActive(true);

        if (!panelActive && !_playerAlive)
        {
            FindBestScore();
            UpdateHighScoreText();
            _scoreText.rectTransform.anchoredPosition = scorePosOnDeath;

            _restartUIPanel.SetActive(true);
        }
        else if (!panelActive && _playerAlive)
        {
            _scoreText.rectTransform.anchoredPosition = scorePosOnDeath;
            _restartBtn.SetActive(false);
            _restartUIPanel.SetActive(true);
        }
        else if (panelActive && _playerAlive)
        {
            _scoreText.rectTransform.anchoredPosition = defaultScorePos;
            _restartUIPanel.SetActive(false);
        }


    }
}
