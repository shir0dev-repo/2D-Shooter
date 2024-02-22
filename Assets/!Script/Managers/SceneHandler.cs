using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
  public static Action<int> OnSceneLoaded;
  public static int CurrentSceneIndex { get; private set; }
  [SerializeField] private Scene _startScene;
  [SerializeField] private Scene _playScene;
  [SerializeField] private Scene _gameOverScene;

  public void LoadScene(int index)
  {
    SceneManager.LoadScene(index);
    CurrentSceneIndex = index;
  }
}