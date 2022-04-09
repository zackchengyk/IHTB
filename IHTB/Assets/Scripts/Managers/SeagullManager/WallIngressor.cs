using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallIngressor : MonoBehaviour
{
  public static WallIngressor Instance;
  
  void Awake() { Instance = this; }

  public void IngressOneWall(
    Edge edge,
    float wallSpacing,
    float wallOffset,
    Vector2 wallVelocity)
  {
    (Vector2 center, Vector2 parallel, int halfCount) tuple = getWallParams(edge, wallSpacing);

    spawnWall(
      tuple.center,
      tuple.parallel,
      tuple.halfCount,
      wallSpacing,
      wallOffset,
      wallVelocity);
  }

  public void IngressMultiWall(
    Edge    edge,
    float   wallSpacing,
    Vector2 wallVelocity,
    int     wallCount)
  {
    StartCoroutine(ingressMultiWallHelper(edge, wallSpacing, wallVelocity, wallCount));
  }

  private IEnumerator ingressMultiWallHelper(
    Edge    edge,
    float   wallSpacing,
    Vector2 wallVelocity,
    int     wallCount)
  {
    float wallOffset = wallSpacing / 2;
    
    for (int i = 0; i < wallCount; ++i)
    {
      IngressOneWall(edge, wallSpacing, i % 2 == 0 ? 0 : wallOffset, wallVelocity);
      yield return new WaitForSeconds(Mathf.Abs(wallSpacing / wallVelocity.magnitude));
    }
  }

  private (Vector2, Vector2, int) getWallParams(Edge edge, float wallSpacing)
  {
    Vector2 screenDimensions = SeagullManagerScript.Instance.ScreenDimensions;

    (Vector2 center, Vector2 parallel, int halfCount) tuple;
    
    switch (edge)
    {
      case Edge.leftOnly:
      {
        tuple.center = new Vector2(-screenDimensions.x, 0);
        tuple.parallel = Vector2.up;
        tuple.halfCount = (int) (screenDimensions.y / wallSpacing) + 1;
        return tuple;
      }
      default:
      {
        tuple.center = new Vector2(0, screenDimensions.y);
        tuple.parallel = Vector2.right;
        tuple.halfCount = (int) (screenDimensions.x / wallSpacing) + 1;
        return tuple;
      }
    }
  }

  private void spawnWall(
    Vector2 center,
    Vector2 parallel,
    int     halfCount,
    float   wallSpacing,
    float   wallOffset,
    Vector2 wallVelocity)
  {
    for (int i = -halfCount; i <= halfCount; ++i)
    {
      spawnLinearSeagull(
        center + parallel * (wallSpacing * i + wallOffset),
        wallVelocity);
    }
  }

  private void spawnLinearSeagull(Vector3 position, Vector3 velocity)
  {
    GameObject obj = ObjectPooler.Instance.GetPooledObject(SeagullIndex.Linear);

    obj.GetComponent<SeagullBehaviour>().ResetWhenTakenFromPool(position, velocity);
    obj.SetActive(true);
  }
}

// public class WallIngressor : MonoBehaviour
// {
  // [SerializeField] protected GameObject _linearSeagull;
  
  // private Vector2 _screenDimensions;
  // private Vector2 _centerTopPosition;

  // void Start()
  // {
  //   _screenDimensions  = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
  //   _centerTopPosition = new Vector2(0, _screenDimensions.y);

  //   StartCoroutine(spawnWalls());
  // }

  // void FixedUpdate() {}

  // /* WALLS */

  // [SerializeField] private Vector2 _wallVelocity = Vector2.down * 3.0f;

  // private IEnumerator spawnWalls()
  // {
  //   float spacing = 6;
  //   int offset = 3;
  //   while (PlayerManagerScript.Instance.PlayerIsAlive)
  //   {
  //     yield return new WaitForSeconds(Mathf.Abs(spacing / _wallVelocity.y));
  //     GenerateWall(spacing, offset);
  //     offset = offset == 3 ? 0 : 3;
  //   }
  // }

  // private void GenerateWall(float spacing, float offset)
  // {
  //   int halfCount = (int)(_screenDimensions.x / spacing) + 1;

  //   for (int i = -halfCount; i <= halfCount; ++i)
  //   {
  //     SpawnSeagull(
  //       _linearSeagull,
  //       _centerTopPosition + Vector2.right * (spacing * i + offset),
  //       _wallVelocity);
  //   }
  // }

  // /* Spawning */

  // private void SpawnSeagull(GameObject seagull, Vector3 position, Vector3 velocity)
  // {
  //   GameObject obj = ObjectPooler.Instance.GetPooledObject(0);

  //   obj.GetComponent<SeagullBehaviour>().ResetWhenTakenFromPool(position, velocity);
  //   obj.SetActive(true);
  // }
// }
