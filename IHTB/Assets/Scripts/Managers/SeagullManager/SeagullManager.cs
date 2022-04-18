using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeagullIndex : int
{
  Linear = 0,
  Curved = 1,
  Sniper = 2,
  Homing = 3,
  RandomSpawner = 4,
}

[DisallowMultipleComponent]
public class SeagullManager : MonoBehaviour
{
  public static SeagullManager Instance;

  private Vector2       _screenDimensions;
  private IngressorType _currIngressorType;
  private IngressorType _nextIngressorType;

  private float _difficulty;
  private float _maxDifficultyTime = 120.0f;
  private float _startTime;

  // ================== Accessors

  public Vector2 ScreenDimensions { get { return _screenDimensions; } }

  // ================== Methods
  
  void Awake() { Instance = this; }

  void Start()
  {
    _screenDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    _nextIngressorType = getRandomIngressorType();

    _difficulty = 0.0f;
    _startTime = Time.time;

    StartCoroutine(enterIngressors());
  }

  void FixedUpdate()
  {
    _difficulty = Mathf.Clamp((Time.time - _startTime) / _maxDifficultyTime, 0.0f, 1.0f);
    Debug.Log(_difficulty);
  }

  private IEnumerator enterIngressors()
  {
    while (PlayerManager.Instance.PlayerIsAlive)
    {
      // Select next ingressor
      _currIngressorType = _nextIngressorType;
      _nextIngressorType = getRandomIngressorType();

      // Call appropriate ingressor
      float waitTime = callIngressor(_currIngressorType);

      // Wait between ingresses
      yield return new WaitForSeconds(waitTime);
    }
  }

  // ================== Helpers

  private IngressorType getRandomIngressorType() { return (IngressorType) Random.Range(0, 1); }

  private float callIngressor(IngressorType ingressorType)
  {
    if (ingressorType == IngressorType.Random) ingressorType = getRandomIngressorType();
    switch (ingressorType)
    {
      case IngressorType.Spawner: return SpawnerIngressor.Instance.Ingress(_difficulty);
      case IngressorType.Wall:
        return WallIngressor.Instance.IngressMultiWall(edge: Edge.TopOnly,
                                                       wallSpacing: Random.Range(2, 4),
                                                       wallVelocity: ScrollManager.Instance.ScrollVelocity,
                                                       wallCount: Random.Range(2, 4));
      default:
        return 0.0f;
    }
  }
}