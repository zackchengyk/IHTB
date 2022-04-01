using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To delete

public class WallSpawner : SeagullSpawner
{
  protected override void EnableSeagullSpawner()
  {
    int xGap = Random.Range(-5, 2);
    for (int i = -8; i<8; i++)
    {
      if (i != xGap)
      {
        SpawnSeagull(this.transform.position + Vector3.right * i);
      }
    }
  }

  protected override void UpdateSeagullSpawner() {}

  private void SpawnSeagull(Vector3 pos)
  {
    // GameObject obj = Instantiate(_seagull, pos, Quaternion.identity) as GameObject;
    // obj.GetComponent<SeagullBehaviour>()._initialVelocity = new Vector2(0f, -1f) * 3.0f;
  }

  protected override void DisableSeagullSpawner() {}
}
