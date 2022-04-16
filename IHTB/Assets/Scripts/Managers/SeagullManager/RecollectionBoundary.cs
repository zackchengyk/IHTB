using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class RecollectionBoundary : MonoBehaviour
{
  void OnTriggerExit2D(Collider2D other) { other.gameObject.SetActive(false); }
}
