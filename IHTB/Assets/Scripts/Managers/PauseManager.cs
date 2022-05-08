using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class PauseManager : MonoBehaviour
{
  public static PauseManager Instance;

  [SerializeField] private GameObject _pauseMenuCanvas;

  void Awake()
  {
    Instance = this;
    _pauseMenuCanvas.SetActive(false);
  }

  public void OpenPauseMenu()
  {
    _pauseMenuCanvas.SetActive(true);
    if (!_pauseMenuCanvas.activeSelf)
    {
        _pauseMenuCanvas.SetActive(true);
    }
  }

  public void ClosePauseMenu()
  {
    InputManager.Instance.Pausing = false;
    Time.timeScale = 1;
    _pauseMenuCanvas.SetActive(false);
  }

  // this function toggles the pause menu on/off
  public void TogglePauseMenu(bool Pausing)
  {
    if (Pausing)
    {
      OpenPauseMenu();
    }
    else
    {
      ClosePauseMenu();
    }
  }

  public void Retry()
  {
    InputManager.Instance.Pausing = false;
    Time.timeScale = 1;
    IHTBSceneManager.Instance.ToGameScene();
  }

  public void ToMain()
  {
    InputManager.Instance.Pausing = false;
    Time.timeScale = 1;
    IHTBSceneManager.Instance.ToMainMenuScene();
  }
}
