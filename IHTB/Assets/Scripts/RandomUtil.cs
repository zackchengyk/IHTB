using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class RandomUtil 
{
  public static float Triangular(float minInclusive, float maxExclusive)
  {
    float halfMin = minInclusive / 2.0f;
    float halfMax = maxExclusive / 2.0f;
    return Random.Range(halfMin, halfMax) + Random.Range(halfMin, halfMax);
  }
}