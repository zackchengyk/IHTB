using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class TimeManager : MonoBehaviour
{
  public static TimeManager Instance;

  // ================== Methods

  void Awake() { Instance = this; }

  // Starts a coroutine to manage a temporary timescale change in real time
  public void SlowTimeNonLinearRealtime(float minTimeScale, float realDuration)
  {
    StopAllCoroutines();
    StartCoroutine(slowTimeNonLinearRealtime(minTimeScale, realDuration));
  }

  // ================== Helpers

  private IEnumerator slowTimeNonLinearRealtime(float minTimeScale, float realDuration)
  {
    float startTime = Time.realtimeSinceStartup;
    float endTime = startTime + realDuration;

    while (Time.realtimeSinceStartup < endTime)
    {
      yield return null;

      float t = Mathf.InverseLerp(startTime, endTime, Time.realtimeSinceStartup);
      t = -(Mathf.Sqrt(1f - (t * t)) - 1f);

      Time.timeScale = t * (1 - minTimeScale) + minTimeScale;
    }

    Time.timeScale = 1;
  }
}
