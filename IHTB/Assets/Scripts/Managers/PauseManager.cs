using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class PauseManager : MonoBehaviour
{
  public static PauseManager Instance;

  [SerializeField] private GameObject _pauseMenuCanvas;

  private bool _isLoadingNextScene = false;
  private bool _pausing = false;

  // ================== Accessors

  public bool Pausing { get { return _pausing; } }

  // ================== Methods

  void Awake()
  {
    Instance = this;
    _pauseMenuCanvas.SetActive(false);
  }

  public void OpenPauseMenu()
  {
    Time.timeScale = 0;
    _pausing = true;

    _pauseMenuCanvas.SetActive(true);
    if (!_pauseMenuCanvas.activeSelf) _pauseMenuCanvas.SetActive(true);
  }

  public void ClosePauseMenu()
  {
    Time.timeScale = 1;
    _pausing = false;
    _pauseMenuCanvas.SetActive(false);
  }

  // This function toggles the pause menu on/off
  public void TogglePauseMenu()
  {
    if (_isLoadingNextScene) return;

    if (_pausing)
    {
      ClosePauseMenu();
    }
    else
    {
      OpenPauseMenu();
    }
  }

  public void Retry()
  {
    if (_isLoadingNextScene) return;
    _isLoadingNextScene = true;

    Time.timeScale = 1;
    _pausing = false;
    IHTBSceneManager.Instance.ToGameScene();
  }

  public void ToMain()
  {
    if (_isLoadingNextScene) return;
    _isLoadingNextScene = true;

    Time.timeScale = 1;
    _pausing = false;
    IHTBSceneManager.Instance.ToMainMenuScene();
  }
}
