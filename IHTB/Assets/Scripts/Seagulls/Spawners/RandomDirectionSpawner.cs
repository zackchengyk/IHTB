using UnityEngine;

public class RandomDirectionSpawner : SeagullSpawner
{
  // Start is called before the first frame update
  protected override void Start()
  {
    base.Start();
    InvokeRepeating("SpawnSeagull", 0.0f, 0.5f);
  }

  // Call this to spawn a seagull
  private void SpawnSeagull()
  {
    GameObject obj = Instantiate(_seagull, this.transform.position, Quaternion.identity) as GameObject;
    obj.GetComponentInChildren<SeagullBehaviour>().initialVelocity = RandomUnitVector() * 3.0f + scrollVelocity;
  }

  // Helper
  private Vector2 RandomUnitVector()
  {
    float random = Random.Range(0.0f, 2.0f * Mathf.PI);
    return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
  }
}
