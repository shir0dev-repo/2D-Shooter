using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
  public static Action<int> OnSceneLoaded;

  private int _currentSceneIndex = 0;

  public int CurrentSceneIndex
  {
    get { return _currentSceneIndex; }
  }

  private void OnEnable()
  {
    GameManager.OnPlayerDeath += () => { LoadScene(2); };
  }

  private void Start()
  {
    OnSceneLoaded?.Invoke(_currentSceneIndex);
  }

  public void LoadScene(int index)
  {
    SceneManager.LoadScene(index);
    _currentSceneIndex = index;
    OnSceneLoaded?.Invoke(_currentSceneIndex);
  }
}