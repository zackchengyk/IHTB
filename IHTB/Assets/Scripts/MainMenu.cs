using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
  [SerializeField] private GameObject _instructionsCanvas;
  [SerializeField] private GameObject _instructions1;
  [SerializeField] private GameObject _instructions2;
  [SerializeField] private GameObject _closeButton;
  [SerializeField] private GameObject _prevButton;
  [SerializeField] private GameObject _nextButton;
  [SerializeField] private GameObject _creditsCanvas;

  void Awake()
  {
    CloseInstructions();
    CloseCredits();
  }

  // ================== GAME

  public void StartGame() { IHTBSceneManager.Instance.ToGameScene(); }
  public void ExitGame()  { IHTBSceneManager.Instance.ExitGame(); }

  // ================== CREDITS

  public void OpenCredits()  { _creditsCanvas.SetActive(true); }
  public void CloseCredits() { _creditsCanvas.SetActive(false); }

  // ================== INSTRUCTIONS

  public void OpenInstructions() { _instructionsCanvas.SetActive(true); }

  public void CloseInstructions()
  {
    _instructionsCanvas.SetActive(false);
    _instructions1.SetActive(true);
    _instructions2.SetActive(false);
    _closeButton.SetActive(true);
    _prevButton.SetActive(false);
    _nextButton.SetActive(true);
  }

  public void NextInstructions()
  {
    _instructions2.SetActive(true);
    _prevButton.SetActive(true);
    _nextButton.SetActive(false);
  }

  public void PrevInstructions()
  {
    _instructions2.SetActive(false);
    _prevButton.SetActive(false);
    _nextButton.SetActive(true);
  }
}
