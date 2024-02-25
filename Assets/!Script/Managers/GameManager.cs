using System;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Serializable]
    private class SaveData
    {
        public int SavedScore;
    }

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
        OnPlayerDeath += SaveGame;
    }

    private void Awake()
    {
        LoadGame();
    }

    private void OnDisable()
    {
        SceneHandler.OnSceneLoaded -= SpawnPlayer;

        OnGameRestart -= ResetGameState;
        OnScoreIncremented -= IncrementScore;
        OnPlayerDeath -= KillPlayer;
        OnPlayerDeath -= SaveGame;
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

    public void SaveGame()
    {
        SaveData data = new()
        {
            SavedScore = _highScore
        };

        string filePath = Application.persistentDataPath + "/savedScore.json";
        string json = JsonUtility.ToJson(data, true);

        Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            int previouslySavedScore = JsonUtility.FromJson<SaveData>(File.ReadAllText(filePath)).SavedScore;
            if (_highScore > previouslySavedScore)
            {
                File.WriteAllText(filePath, json);
            }
        }
        else
        {
            File.WriteAllText(filePath, json);
        }
    }

    public void LoadGame()
    {
        string filePath = Application.persistentDataPath + "/savedScore.json";

        if (!File.Exists(filePath)) return;

        int loadedScore = JsonUtility.FromJson<SaveData>(File.ReadAllText(filePath)).SavedScore;

        _highScore = Mathf.Max(loadedScore, _highScore);
        MainManager.Instance.UIManager.UpdateHighScoreText(_highScore);
    }
}
