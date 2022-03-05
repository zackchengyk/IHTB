using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Seagull : MonoBehaviour
{
  [SerializeField] protected GameObject _seagull;

  // Start is called before the first frame update
  void Start()
  {
    Debug.Log("Seagull spawned!");
  }

  // Update is called once per frame
  void Update()
  {
    UpdateOwnPosition();
  }

  protected abstract void UpdateOwnPosition();
}
