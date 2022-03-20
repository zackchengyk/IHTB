using UnityEngine;

public class RandomDirectionSpawner : SeagullSpawner
{
  [SerializeField] private float _spawnDelay  = 0.0f;
  [SerializeField] private float _spawnPeriod = 0.5f;
  [SerializeField] private float _spawnInitialSpeed = 3.0f;

  protected override void StartSeagullSpawner()
  {
    InvokeRepeating("SpawnSeagull", _spawnDelay, _spawnPeriod);
  }

  protected override void UpdateSeagullSpawner() {}

  // Helper: call this to spawn a seagull with a random initial velocity
  private void SpawnSeagull()
  {
    GameObject obj = Instantiate(_seagull, this.transform.position, Quaternion.identity) as GameObject;
    obj.GetComponentInChildren<SeagullBehaviour>().initialVelocity = RandomUnitVector() * _spawnInitialSpeed + scrollVelocity;
  }

  // Helper: get a random unit vector
  private Vector2 RandomUnitVector()
  {
    float random = Random.Range(0.0f, 2.0f * Mathf.PI);
    return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
  }
}
