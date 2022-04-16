using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SpawnerIngressor : MonoBehaviour
{
  public static SpawnerIngressor Instance;

  // ================== Methods
  
  void Awake() { Instance = this; }

  public void IngressSpawner(SeagullIndex spawnerIndex, SeagullIndex seagullIndex, Vector2 position)
  {
    GameObject obj = ObjectPooler.Instance.GetPooledObject(spawnerIndex);

    obj.GetComponent<SeagullSpawner>().ResetWhenTakenFromPool(position, seagullIndex);
    obj.SetActive(true);
  }
}