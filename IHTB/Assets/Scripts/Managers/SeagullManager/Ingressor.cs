using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngressorType : int
{
  Spawner = 0,
  Wall    = 1,
  Random  = 2,
}

public abstract class Ingressor : MonoBehaviour
{
  // Returns event duration; must be overridden by concrete Ingressor
  public abstract float Ingress(float difficulty);
}
