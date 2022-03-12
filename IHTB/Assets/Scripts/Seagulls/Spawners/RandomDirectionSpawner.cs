using UnityEngine;

public class RandomDirectionSpawner : SeagullSpawner
{
  protected override void StartSeagullSpawner()
  {
    InvokeRepeating("SpawnSeagull", 0.0f, 0.5f);
  }

  protected override void UpdateSeagullSpawner() {
    Debug.Log(PlayerManagerScript.Instance.GetPlayerPosition());
  }

  // Helper: call this to spawn a seagull with a random initial velocity
  private void SpawnSeagull()
  {
    GameObject obj = Instantiate(_seagull, this.transform.position, Quaternion.identity) as GameObject;
    obj.GetComponentInChildren<SeagullBehaviour>().initialVelocity = RandomUnitVector() * 3.0f + scrollVelocity;
  }

  // Helper: get a random unit vector
  private Vector2 RandomUnitVector()
  {
    float random = Random.Range(0.0f, 2.0f * Mathf.PI);
    return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
  }
}
