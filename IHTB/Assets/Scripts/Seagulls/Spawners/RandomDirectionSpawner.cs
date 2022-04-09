using UnityEngine;

public class RandomDirectionSpawner : SeagullSpawner
{
  protected override void EnableSeagullSpawner()
  {
    InvokeRepeating("SpawnRandomDirectionSeagull", SpawnDelay, SpawnPeriod);
  }

  protected override void UpdateSeagullSpawner() {}

  protected override void DisableSeagullSpawner() { CancelInvoke(); }

  // Helper: call this to spawn a seagull with a random initial velocity
  private void SpawnRandomDirectionSeagull()
  {
    SpawnSeagull(_rigidbody2D.position, randomUnitVector() * SpawnInitialSpeed);
  }

  // Helper: get a random unit vector
  private Vector2 randomUnitVector()
  {
    float random = Random.Range(0.0f, 2.0f * Mathf.PI);
    return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
  }
}
