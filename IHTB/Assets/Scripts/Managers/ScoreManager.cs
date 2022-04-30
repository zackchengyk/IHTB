using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DisallowMultipleComponent]
public class ScoreManager : MonoBehaviour
{
  public static ScoreManager Instance;

  private float _score = 0f;
  private int _multiplier = 1;
  private TextMeshProUGUI _scoreTextMesh;
  private TextMeshProUGUI _multiplierTextMesh;
  private Animator _multiplierAnimator;

  // ================== Accessors

  public float Score
  {
    get { return _score; }
    set {
      _score = value;
      _scoreTextMesh.text = "SCORE: " + Mathf.Round(value);
    }
  }

  public int Multiplier 
  { 
    get { return _multiplier; } 
    set {
      int newValue = Mathf.Max(value, 1);
      _multiplier = newValue;
      _multiplierTextMesh.text = "Multiplier: x" + newValue + new string('!', newValue / 5);
    } 
  }

  // ================== Methods

  void Awake()
  {
    Instance = this;
    _score = 0f;
    _multiplier = 1;
    var textMeshes = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
    _scoreTextMesh = textMeshes[0];
    _multiplierTextMesh = textMeshes[1];
    _multiplierAnimator = gameObject.GetComponentInChildren<Animator>();
  }

  void FixedUpdate()
  {
    Score += ScrollManager.Instance.ScrollVelocity.magnitude * Time.deltaTime * _multiplier;
  }

  public void ResetScore() { Score = 0; }

  public void UpMultiplier(int rolledThroughCount)
  {
    int newValue = this.Multiplier + rolledThroughCount * rolledThroughCount;
    this.Multiplier = newValue;
    Debug.Log("Multiplier " + Multiplier + "!");
    switch (rolledThroughCount) {
      case 0: break;
      case 1: _multiplierAnimator.SetTrigger("Expand_1_25"); break;
      case 2: _multiplierAnimator.SetTrigger("Expand_1_75"); break;
      default: _multiplierAnimator.SetTrigger("Expand_2_5"); break;
    }
  }

  public void DownMultiplier()
  {
    this.Multiplier = Mathf.Max(1, this.Multiplier / 2);
    Debug.Log("Multiplier lost!");
  }
}
