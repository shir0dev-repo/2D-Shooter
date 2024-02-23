using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
  public static Action<int> OnSceneLoaded;
  public static readonly int StartSceneIndex = 0;
  public static readonly int PlaySceneIndex = 1;
  public static readonly int GameOverSceneIndex = 2;
  private Scene _startScene;

  /*

  - game starts, reference to startscene is saved
  - on press play, play scene loads and activates, but start scene is NOT unloaded.
  - when press main menu, the scene SWITCHES to startscene, not reload it

  */

  private int _currentSceneIndex = 0;

  public int CurrentSceneIndex
  {
    get { return _currentSceneIndex; }
  }

  private void OnEnable()
  {
    GameManager.OnPlayerDeath += LoadDeathScene;
  }

  private void Start()
  {
    _startScene = SceneManager.GetActiveScene();
    OnSceneLoaded?.Invoke(_currentSceneIndex);
  }

  private void OnDisable()
  {
    GameManager.OnPlayerDeath -= LoadDeathScene;
  }

  private void LoadDeathScene()
  {
    if (_currentSceneIndex == 1)
      LoadScene(2);
  }

  public void LoadScene(int index)
  {
    //SceneManager.LoadScene(index);
    //_currentSceneIndex = index;
    //OnSceneLoaded?.Invoke(_currentSceneIndex);
    StartCoroutine(LoadSceneCoroutine(index));
  }

  private IEnumerator LoadSceneCoroutine(int index)
  {
    AsyncOperation loadOp = SceneManager.LoadSceneAsync(index);
    loadOp.allowSceneActivation = true;

    while (loadOp.progress < 1f)
    {
      yield return new WaitForEndOfFrame();
    }

    _currentSceneIndex = index;
    OnSceneLoaded?.Invoke(_currentSceneIndex);
  }
}