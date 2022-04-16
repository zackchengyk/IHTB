using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ScoreManager : MonoBehaviour
{
  public static ScoreManager Instance;

  double score = 0f;

  // ================== Methods

  void Awake() { Instance = this; }

  void Update()
  {
    score += ScrollManager.Instance.ScrollVelocity.magnitude * Time.deltaTime;

    Debug.Log("Your Score: " + (int)score);
  }
}
