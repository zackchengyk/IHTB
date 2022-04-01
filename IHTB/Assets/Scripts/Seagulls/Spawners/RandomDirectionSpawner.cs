using UnityEngine;

public class RandomDirectionSpawner : SeagullSpawner
{
  [SerializeField] private float _spawnDelay  = 0.0f;
  [SerializeField] private float _spawnPeriod = 0.75f;
  [SerializeField] private float _spawnInitialSpeed = 3.5f;

  protected override void EnableSeagullSpawner()
  {
    InvokeRepeating("SpawnRandomDirectionSeagull", _spawnDelay, _spawnPeriod);
  }

  protected override void UpdateSeagullSpawner() {}

  protected override void DisableSeagullSpawner()
  {
    CancelInvoke();
  }

  // Helper: call this to spawn a seagull with a random initial velocity
  private void SpawnRandomDirectionSeagull()
  {
    SpawnSeagull(_rigidbody2D.position, RandomUnitVector() * _spawnInitialSpeed);
  }

  // Helper: get a random unit vector
  private Vector2 RandomUnitVector()
  {
    float random = Random.Range(0.0f, 2.0f * Mathf.PI);
    return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
  }
}
