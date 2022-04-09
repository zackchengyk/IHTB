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

public enum IngressorType : int
{
  Spawner = 0,
  Wall    = 1,
  Random  = 2,
}

public enum Edge : int
{
  topOnly    = 0,
  bottomOnly = 1,
  leftOnly   = 2,
  rightOnly  = 3,
  any        = 4, 
}

public class SeagullManagerScript : MonoBehaviour
{
  public static SeagullManagerScript Instance;

  private Vector2 _screenDimensions;
  private IngressorType currIngressorType;
  private IngressorType nextIngressorType;

  // Accessors
  public Vector2 ScreenDimensions { get { return _screenDimensions; } }

  void Awake() { Instance = this; }

  void Start()
  {
    _screenDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

    nextIngressorType = getNextIngressorType();

    StartCoroutine(enterIngressors());
  }

  private IEnumerator enterIngressors()
  {
    while (PlayerManagerScript.Instance.PlayerIsAlive)
    {
      // Select next ingressor
      currIngressorType = nextIngressorType;
      nextIngressorType = getNextIngressorType();
      float waitTime = getWaitTime();

      // Call appropriate ingressor
      callCurrentIngressor();

      // Wait between ingress events
      yield return new WaitForSeconds(waitTime);
    }
  }

  // ================== Helpers

  private IngressorType getNextIngressorType()
  {
    return (IngressorType) Random.Range(0, 2);
  }

  private float getWaitTime()
  {
    switch (currIngressorType)
    {
      case IngressorType.Spawner:
      {
        switch (nextIngressorType)
        {
          case IngressorType.Spawner: return Random.Range(0.25f, 2f);
          case IngressorType.Wall:    return Random.Range(2.5f,  5f);
          default:                    return Random.Range(0.25f, 2f);
        }
      }
      case IngressorType.Wall:
      {
        switch (nextIngressorType)
        {
          case IngressorType.Spawner: return Random.Range(0.25f, 2f);
          case IngressorType.Wall:    return Random.Range(3f, 4f);
          default:                    return Random.Range(0.25f, 2f);
        }
      }
      default:
      {
        switch (nextIngressorType)
        {
          case IngressorType.Spawner: return Random.Range(0.25f, 2f);
          case IngressorType.Wall:    return Random.Range(0.25f, 2f);
          default:                    return Random.Range(0.25f, 2f);
        }
      }
    }
  }

  private void callCurrentIngressor()
  {
    switch (currIngressorType)
    {
      case IngressorType.Spawner:
      {
        SpawnerIngressor.Instance.IngressSpawner(
          SeagullIndex.RandomSpawner,
          SeagullIndex.Homing,
          getRandomPositionAlongEdge(Edge.topOnly));
        return;
      }
      case IngressorType.Wall:
      {
        WallIngressor.Instance.IngressMultiWall(
          edge: Edge.topOnly,
          wallSpacing: Random.Range(2, 4),
          wallVelocity: ScrollManagerScript.Instance.ScrollVelocity,
          wallCount: Random.Range(2, 4));
        return;
      }
      default:
      {
        return;
      }
    }
  }

  Vector2 getRandomPositionAlongEdge(Edge edge)
  {
    switch (edge)
    {
      case Edge.topOnly:
      {
        return new Vector2(
          _screenDimensions.x * Random.Range(-1f, 1f),
          _screenDimensions.y);
      }
      default:
      {
        return Vector2.zero;
      }
    }
  }
}