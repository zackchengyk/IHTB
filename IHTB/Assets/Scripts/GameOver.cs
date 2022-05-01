using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _finalScoreTextMesh;

  void Awake() { _finalScoreTextMesh.text = "SCORE: " + IHTBSceneManager.Instance.FinalScore; }

  public void StartGame() { IHTBSceneManager.Instance.ToGameScene(); }
  public void ToMain()    { IHTBSceneManager.Instance.ToMainMenuScene(); }
  public void ExitGame()  { IHTBSceneManager.Instance.ExitGame(); }
}
