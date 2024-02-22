using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] Transform _spawnPoint;

    public Action<int> OnScoreIncremented;
    public static Action OnPlayerDeath { get; set; }
    public static Action OnGameRestart { get; set; }
    public Vector3 PlayerPosition => GetPlayerPosition();
    public bool PlayerAlive => _playerAlive;

    private bool _playerAlive = false;
    private int _currentScore = 0;
    private int _highScore = 0;

    Vector3 _lastStoredPosition = Vector3.zero;
    PlayerMovement _playerMovement;

    private void Start()
    {
        MainManager.Instance.UIManager.UpdateScoreText(0);
    }

    private void OnEnable()
    {
        SceneHandler.OnSceneLoaded += SpawnPlayer;

        OnGameRestart += ResetScore;
        OnScoreIncremented += IncrementScore;
        OnPlayerDeath += KillPlayer;

        _playerAlive = true;
    }

    private void OnDisable()
    {
        SceneHandler.OnSceneLoaded -= SpawnPlayer;

        OnGameRestart -= ResetScore;
        OnScoreIncremented -= IncrementScore;
        OnPlayerDeath -= KillPlayer;
    }

    void KillPlayer()
    {
        _playerAlive = false;
        if (NewHighScore())
            MainManager.Instance.UIManager.UpdateHighScoreText(_highScore);
        MainManager.Instance.UIManager.ToggleUI();
    }

    void ResetScore()
    {
        _currentScore = 0;
        MainManager.Instance.UIManager.UpdateScoreText(0);
    }

    void IncrementScore(int score)
    {
        _currentScore += score;

        MainManager.Instance.UIManager.UpdateScoreText(_currentScore);
    }
    bool NewHighScore()
    {
        if (_currentScore > _highScore)
            _highScore = _currentScore;

        // if current score is assigned to high score, it is a new high score, so true
        return _highScore == _currentScore;
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

    void SpawnPlayer(int sceneIndex)
    {
        // 1 is build order of play scene
        if (sceneIndex != 1)
            return;

        GameObject player = Instantiate(_playerPrefab, _spawnPoint.position, Quaternion.identity);
        _playerMovement = player.GetComponent<PlayerMovement>();
        _ = GetPlayerPosition();
    }
}
