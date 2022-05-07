using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    [SerializeField] private GameObject _pauseMenuCanvas;

    void Awake() {
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

    public void ClosePauseMenu() {
        InputManager.Instance.Pausing = false;
        Time.timeScale = 1;
        _pauseMenuCanvas.SetActive(false);
    }

    //this function toggles the pause menu on/off
    public void TogglePauseMenu(bool Pausing)
    {
        //Debug.Log("got into TogglePauseMenu");
        //if the game is paused
        if (Pausing)
        {
            //Debug.Log("Pausing == true");
            OpenPauseMenu();
        } else //if the game is continued
        {
            //Debug.Log("Pausing == false");
            ClosePauseMenu();
        }
    }

    public void ToMain() {
        InputManager.Instance.Pausing = false;
        Time.timeScale = 1;
        IHTBSceneManager.Instance.ToMainMenuScene(); }
}
