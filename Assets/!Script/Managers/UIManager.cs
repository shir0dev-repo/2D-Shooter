using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
  private const string SCORE_PREFIX = "Score: ";
  private const string HIGH_SCORE_PREFIX = "Best: ";

  [SerializeField] TextMeshProUGUI _dynamicScoreText;
  [SerializeField] TextMeshProUGUI _staticScoreText;
  [SerializeField] TextMeshProUGUI _highScoreText;
  [Space]
  [SerializeField] GameObject _startPanel;
  [SerializeField] GameObject _pausePanel;
  [SerializeField] GameObject _gameOverPanel;

  private void Awake()
  {
    UpdateScoreText(0);
  }

  private void OnEnable()
  {
    SceneHandler.OnSceneLoaded += UpdateUI;
  }

  private void OnDisable()
  {
    SceneHandler.OnSceneLoaded -= UpdateUI;
  }

  private void UpdateUI(int sceneIndex)
  {
    GetComponentInChildren<Canvas>().worldCamera = Camera.main;

    switch (sceneIndex)
    {
      case 0:
        _dynamicScoreText.gameObject.SetActive(false);
        _startPanel.SetActive(true);
        _pausePanel.SetActive(false);
        _gameOverPanel.SetActive(false);
        break;
      case 1:
        _dynamicScoreText.gameObject.SetActive(true);
        _startPanel.SetActive(false);
        _pausePanel.SetActive(false);
        _gameOverPanel.SetActive(false);
        break;
      case 2:
        _dynamicScoreText.gameObject.SetActive(false);
        _staticScoreText.gameObject.SetActive(true);
        _startPanel.SetActive(false);
        _pausePanel.SetActive(false);
        _gameOverPanel.SetActive(true);
        break;
      default:
        _startPanel.SetActive(false);
        _pausePanel.SetActive(false);
        _gameOverPanel.SetActive(true);
        break;
    }
  }

  public void UpdateScoreText(int currentScore)
  {
    _dynamicScoreText.text = SCORE_PREFIX + currentScore.ToString();
    _staticScoreText.text = SCORE_PREFIX + currentScore.ToString();
  }
  public void UpdateHighScoreText(int newHighScore) => _highScoreText.text = HIGH_SCORE_PREFIX + newHighScore.ToString();

  public void TogglePauseMenu(bool currentlyPaused)
  {
    if (currentlyPaused)
      _pausePanel.SetActive(false);
    else
      _pausePanel.SetActive(true);
  }
}