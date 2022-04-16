using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ingressor : MonoBehaviour
{
  // Must be overridden by concrete Ingressor
  protected abstract void Ingress();
}
