using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action OnPlayerDeath;
    public static Action OnGameRestart;

    public Action<int> OnScoreIncremented;

    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] Transform _spawnPoint;

    private int _currentScore = 0;
    private int _highScore = 0;

    private Vector3 _lastStoredPosition = Vector3.zero;
    private PlayerMovement _playerMovement;

    public Vector3 PlayerPosition => GetPlayerPosition();

    private void OnEnable()
    {
        SceneHandler.OnSceneLoaded += SpawnPlayer;

        OnGameRestart += ResetGameState;
        OnScoreIncremented += IncrementScore;
        OnPlayerDeath += KillPlayer;
    }

    private void OnDisable()
    {
        SceneHandler.OnSceneLoaded -= SpawnPlayer;

        OnGameRestart -= ResetGameState;
        OnScoreIncremented -= IncrementScore;
        OnPlayerDeath -= KillPlayer;
    }

    void ResetGameState()
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

            return _playerMovement.transform.position;
        }

        else
        {
            return _lastStoredPosition;
        }
    }

    void SpawnPlayer(int sceneIndex)
    {
        // 1 is build order of play scene
        if (sceneIndex != 1)
            return;

        _playerMovement = Instantiate(_playerPrefab, _spawnPoint.position, Quaternion.identity).GetComponent<PlayerMovement>();
        _ = GetPlayerPosition();
    }

    void KillPlayer()
    {
        if (NewHighScore())
            MainManager.Instance.UIManager.UpdateHighScoreText(_highScore);
    }
}
