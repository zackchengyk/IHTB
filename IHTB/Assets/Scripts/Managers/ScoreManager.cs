using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ScoreManager : MonoBehaviour
{
  public static ScoreManager Instance;

  private float _score = 0f;

  // ================== Accessors

  public float Score { get { return _score; } }

  // ================== Methods

  void Awake() { Instance = this; }

  void FixedUpdate()
  {
    _score += ScrollManager.Instance.ScrollVelocity.magnitude * Time.deltaTime;
    // Debug.Log("Your Score: " + (int)score);
  }

  public void ResetScore() { _score = 0; }
}
