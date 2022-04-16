using UnityEngine;

public class RandomDirectionSpawner : SeagullSpawner
{
  // ================== Methods

  protected override void EnableSeagullSpawner()
  {
    InvokeRepeating("SpawnRandomDirectionSeagull", _spawnDelay, _spawnPeriod);
  }

  protected override void UpdateSeagullSpawner() {}

  protected override void DisableSeagullSpawner() { CancelInvoke(); }

  // ================== Helpers

  // This spawns a seagull with a random initial velocity
  private void SpawnRandomDirectionSeagull()
  {
    SpawnSeagull(_rigidbody2D.position, randomUnitVector() * _spawnInitialSpeed);
  }

  // This returns a random unit vector
  private Vector2 randomUnitVector()
  {
    float random = Random.Range(0.0f, 2.0f * Mathf.PI);
    return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
  }
}
