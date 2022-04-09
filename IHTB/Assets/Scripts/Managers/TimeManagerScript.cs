using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagerScript : MonoBehaviour
{
  public static TimeManagerScript Instance;

  // Awake is called before the first frame update, whether enabled or not
  void Awake()
  {
    Instance = this;
  }

  // Starts a coroutine to manage a temporary timescale change in real time
  public void SlowTimeNonLinearRealtime(float minTimeScale, float realDuration)
  {
    StopAllCoroutines();
    StartCoroutine(slowTimeNonLinearRealtime(minTimeScale, realDuration));
  }
  private IEnumerator slowTimeNonLinearRealtime(float minTimeScale, float realDuration)
  {
    // Time.timeScale = minTimeScale;
    // yield return new WaitForSecondsRealtime(realDuration);
    // Time.timeScale = 1f;

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
