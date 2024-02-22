using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
  private const string SCORE_PREFIX = "Score: ";
  private const string HIGH_SCORE_PREFIX = "Best: ";

  private static Vector2Int scorePosOnDeath = new Vector2Int(-150, 125);
  private static Vector2Int defaultScorePos = new Vector2Int(-512, 365);

  [SerializeField] TextMeshProUGUI _scoreText;
  [SerializeField] TextMeshProUGUI _highScoreText;
  [Space]
  [SerializeField] GameObject _restartUIPanel;
  [SerializeField] GameObject _restartBtn;
  public void UpdateScoreText(int currentScore) => _scoreText.text = SCORE_PREFIX + currentScore.ToString();
  public void UpdateHighScoreText(int newHighScore) => _highScoreText.text = HIGH_SCORE_PREFIX + newHighScore.ToString();

  public void ToggleUI()
  {
    GameManager gm = MainManager.Instance.GameManager;
    bool panelActive = _restartUIPanel.activeSelf;
    _restartBtn.SetActive(true);

    if (!panelActive && !gm.PlayerAlive)
    {
      _scoreText.rectTransform.anchoredPosition = scorePosOnDeath;

      _restartUIPanel.SetActive(true);
    }
    else if (!panelActive && gm.PlayerAlive)
    {
      _scoreText.rectTransform.anchoredPosition = scorePosOnDeath;
      _restartBtn.SetActive(false);
      _restartUIPanel.SetActive(true);
    }
    else if (panelActive && gm.PlayerAlive)
    {
      _scoreText.rectTransform.anchoredPosition = defaultScorePos;
      _restartUIPanel.SetActive(false);
    }


  }
}