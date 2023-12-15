using System;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] Transform _spawnPoint;
    [Space]
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] GameObject _restartUIPanel;

    public Action<int> OnScoreIncremented;
    public static Action OnPlayerDeath { get; set; }
    public static Action OnGameRestart { get; set; }
    public Vector3 PlayerPosition => GetPlayerPosition();
    public bool PlayerAlive => _playerAlive;

    private bool _playerAlive = false;
    private int _currentScore = 0;
    private const string SCORE_PREFIX = "Score: ";

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

        OnPlayerDeath += ToggleUI;
    }

    private void OnDisable()
    {
        OnScoreIncremented -= IncrementScore;

        OnPlayerDeath -= ToggleUI;
    }

    public void QuitGame()
    {
        if (Application.isPlaying)
            UnityEditor.EditorApplication.isPlaying = false;
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
    }
    void UpdateScoreText() => _scoreText.text = SCORE_PREFIX + _currentScore.ToString();
    void IncrementScore(int score)
    {
        _currentScore += score;

        UpdateScoreText();
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

    void ToggleUI()
    {
        _restartUIPanel.SetActive(!_restartUIPanel.activeSelf);
    }
}
