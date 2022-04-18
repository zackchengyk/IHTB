using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class LivesSystem : MonoBehaviour
{
  [SerializeField] private GameObject _alivePrefab;
  [SerializeField] private GameObject _deadPrefab;

  // Note @Anh: this is a fairly expensive function since you're Destroy-ing and 
  //            Instantiate-ing new things every call; however, since this game only 
  //            has like three lives, it isn't too big a deal; just FYI :)
  public void DisplayLives(int lives, int maxLives)
  {
    // Destroy all current life displays
    foreach (Transform child in transform) Destroy(child.gameObject);

    // Display new life displays
    for (int i = 0; i < maxLives; ++i)
    {
      GameObject life = Instantiate(i < lives ? _alivePrefab : _deadPrefab, transform.position, Quaternion.identity);
      life.transform.SetParent(transform);
      life.gameObject.SetActive(true);
    }
  }
}
