using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class WallIngressor : MonoBehaviour
{
  public static WallIngressor Instance;
  
  // ================== Methods
  
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
  
  // ================== Helpers

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
    Vector2 screenDimensions = SeagullManager.Instance.ScreenDimensions;

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