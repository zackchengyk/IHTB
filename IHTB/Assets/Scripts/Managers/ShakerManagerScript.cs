using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerManagerScript : MonoBehaviour
{
  public static ShakerManagerScript Instance;

  [Tooltip("The thing to shake.")]
  [SerializeField] private Transform _target;

  [Tooltip("The neutral position of the thing to shake.")]
  [SerializeField] private Vector3 _neutral = Vector2.zero;

  [Tooltip("Set this so multiple objects shaking at the same time won't shake the same.")]
  [SerializeField] private Vector2 _seed = Vector2.zero;

  [SerializeField] private float   _defaultFrequencyScale = 15f;
  [SerializeField] private Vector2 _defaultAmplitudeScale = Vector2.one / 4;

  // Internal use
  private float   _frequencyScale;
  private Vector2 _amplitudeScale;
  private float   _currMultiplier;

  // Awake is called before the first frame update, whether enabled or not
  void Awake()
  {
    Instance = this;
  }

  // Update is called once per frame
  void Update()
  {
    // Get a displacement vector
    Vector2 displacement = Vector2.Scale(_amplitudeScale, getNoise(Time.time * _frequencyScale));

    // Scale it for fade-out effects
    displacement *= _currMultiplier;

    // Set the target's transform
    _target.localPosition = _neutral + (Vector3) displacement;
  }

  // Call either of these to shake the screen
  public void ShakeOnceNonLinearRealtime(float realDuration, float frequencyScale, Vector2 amplitudeScale)
  {
    _frequencyScale = frequencyScale;
    _amplitudeScale = amplitudeScale;
    StopAllCoroutines();
    StartCoroutine(shakeOnceNonLinearRealtime(realDuration));
  }
  public void ShakeOnceNonLinearRealtime(float realDuration)
  {
    _frequencyScale = _defaultFrequencyScale;
    _amplitudeScale = _defaultAmplitudeScale;
    StopAllCoroutines();
    StartCoroutine(shakeOnceNonLinearRealtime(realDuration));
  }

  // Helper coroutine. It is responsible for setting enabled and _currMultiplier.
  private IEnumerator shakeOnceNonLinearRealtime(float realDuration)
  {
    enabled = true;
    _currMultiplier = 1f;

    float startTime = Time.realtimeSinceStartup;
    float endTime = startTime + realDuration;

    while (Time.realtimeSinceStartup < endTime)
    {
      yield return null;

      float t = 1f - Mathf.InverseLerp(startTime, endTime, Time.realtimeSinceStartup);

      // Optional: make it non-linear
      t = -(Mathf.Sqrt(1f - (t * t)) - 1f);

      _currMultiplier = t;
    }

    enabled = false;
    _currMultiplier = 0f;
  }

  // Helper to return a noise Vector2 with x and y ranging from -1 to 1.
  private Vector2 getNoise(float time)
  {
    return 2f * new Vector2(
      Mathf.PerlinNoise(_seed.x, time) - 0.5f,
      Mathf.PerlinNoise(time, _seed.y) - 0.5f);
  }

  // Updates _neutral whenever _target is changed in the Unity editor
#if UNITY_EDITOR
  void OnValidate() { if (_target) _neutral = _target.localPosition; }
#endif
}
