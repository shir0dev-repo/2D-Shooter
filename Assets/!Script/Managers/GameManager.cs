using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] TextMeshProUGUI _scoreText;

    public Action<int> OnScoreIncremented;
    public static Action OnPlayerDeath { get; set; }

    public Vector3 PlayerPosition => GetPlayerPosition();

    private Vector3 _lastStoredPosition = Vector3.zero;
    private int _currentScore = 0;
    private const string SCORE_PREFIX = "Score: ";

    protected override void Awake()
    {
        base.Awake();

        UpdateScoreText();
    }

    private void OnEnable()
    {
        OnScoreIncremented += IncrementScore;
    }

    private void OnDisable()
    {
        OnScoreIncremented -= IncrementScore;
    }

    private Vector2 GetPlayerPosition()
    {
        if (_player != null)
        {
            _lastStoredPosition = _player.transform.position;
            return _player.transform.position;
        }

        else
        {
            return _lastStoredPosition;
        }
    }

    void IncrementScore(int score)
    {
        _currentScore += score;

        UpdateScoreText();
    }

    void UpdateScoreText() => _scoreText.text = SCORE_PREFIX + _currentScore.ToString();
}
