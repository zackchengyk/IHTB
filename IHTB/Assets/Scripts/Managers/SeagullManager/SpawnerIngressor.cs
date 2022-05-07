using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SpawnerIngressor : Ingressor
{
  public static SpawnerIngressor Instance;

  // ================== Methods

  void Awake() { Instance = this; }

  public override float Ingress(float difficulty)
  {
    // Select a seagull type based on difficulty
    PooledObjectIndex seagullIndex = selectSeagullIndex(difficulty);

    // Spawn a spawner
    GameObject obj = ObjectPooler.Instance.GetPooledObject(PooledObjectIndex.ScattershotSpawner);
    obj.GetComponent<SeagullSpawner>().ResetWhenTakenFromPool(EdgeUtil.GetRandomPositionAlongEdge(Edge.TopOnly), seagullIndex);
    obj.SetActive(true);

    // Select a wait time
    float waitTime = selectWaitTime(difficulty);

    return waitTime;
  }

  // ================== Helpers

  private PooledObjectIndex selectDebrisIndex()
  {
    return (PooledObjectIndex) Random.Range((int) PooledObjectIndex.PopsicleDebris, (int) PooledObjectIndex.SlushieDebris);
  }

  private PooledObjectIndex selectSeagullIndex(float difficulty)
  {
    return difficulty < Random.value ? PooledObjectIndex.Linear : PooledObjectIndex.Homing;
  }

  private float selectWaitTime(float difficulty)
  {
    return RandomUtil.Triangular(0.5f, 2f) * 3f * (1f - difficulty);
  }
}