using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : SeagullSpawner
{
    protected override void StartSeagullSpawner()
    {
        int xgap = Random.Range(-5, 2);
        scrollVelocity = new Vector2(0f, 0f);
        for(int i = -8; i<8; i++)
        {
            if(i != xgap)
            {
                SpawnSeagull(this.transform.position + Vector3.right * i);
            }
        }
    }

    protected override void UpdateSeagullSpawner() { }

    private void SpawnSeagull(Vector3 pos)
    {
        GameObject obj = Instantiate(_seagull, pos, Quaternion.identity) as GameObject;
        obj.GetComponentInChildren<SeagullBehaviour>().initialVelocity = new Vector2(0f, -1f) * 3.0f;
    }
}
