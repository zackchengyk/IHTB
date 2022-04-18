using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Edge : int
{
  TopOnly    = 0,
  BottomOnly = 1,
  LeftOnly   = 2,
  RightOnly  = 3,
  Random     = 4,
}

static class EdgeUtil
{
  public static Edge GetRandomEdge() { return (Edge) Random.Range(0, 4); }

  public static Vector2 GetRandomPositionAlongEdge(Edge edge)
  {
    Vector2 screenDimensions = SeagullManager.Instance.ScreenDimensions;
    if (edge == Edge.Random) edge = GetRandomEdge();
    switch (edge)
    {
      case Edge.TopOnly:    return new Vector2(screenDimensions.x * Random.Range(-1f, 1f),  screenDimensions.y);
      case Edge.BottomOnly: return new Vector2(screenDimensions.x * Random.Range(-1f, 1f), -screenDimensions.y);
      case Edge.LeftOnly:   return new Vector2(screenDimensions.x, -screenDimensions.y * Random.Range(-1f, 1f));
      case Edge.RightOnly:  return new Vector2(screenDimensions.x,  screenDimensions.y * Random.Range(-1f, 1f));
      default:              return Vector2.zero;
    }
  }
}