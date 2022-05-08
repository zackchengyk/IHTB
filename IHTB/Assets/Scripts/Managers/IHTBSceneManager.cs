using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class IHTBSceneManager : MonoBehaviour
{
  public static IHTBSceneManager Instance;

  private Animator _animator;
  private float _screenWipeDuration = 0.4f;
  private int _finalScore = 0;
  private bool _sceneChanging = false; // some kind of mutex

  // ================== Accessors

  public int FinalScore { get { return _finalScore; } }

  // ================== Methods

  void Awake()
  {
    // There can only be one.
    if (Instance != null)
    {
      Object.Destroy(gameObject);
      return;
    }
    
    Instance = this;
    DontDestroyOnLoad(this);
    _animator = GetComponentInChildren<Animator>();
    _animator.speed = 1 / _screenWipeDuration;
  }

  public void ToMainMenuScene() { StartCoroutine(toScene(0)); }

  public void ToGameScene()     { StartCoroutine(toScene(1)); }

  public void ToGameOverScene(int finalScore)
  {
    _finalScore = finalScore;
    StartCoroutine(toScene(2));
  }

  public void ExitGame()
  {
    Debug.Log("Exiting game!");
    Application.Quit();
  }

  // ================== Helpers

  IEnumerator toScene(int i)
  {
    if (_sceneChanging) yield break;
    _sceneChanging = true;
    _animator.SetTrigger("SceneChangeTrigger");
    yield return new WaitForSeconds(_screenWipeDuration);
    SceneManager.LoadScene(i);
    _sceneChanging = false;
  }
}
