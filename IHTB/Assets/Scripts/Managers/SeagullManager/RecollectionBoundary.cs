using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecollectionBoundary : MonoBehaviour
{
  void OnTriggerExit2D(Collider2D other) { other.gameObject.SetActive(false); }
}
