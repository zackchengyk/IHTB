using UnityEngine;

public class ScattershotSpawner : SeagullSpawner
{
  private Vector2 _currentAimDirection;
  private const float _radiansPerTime = Mathf.PI / 3;

  private const float _spreadAngle = 0.1f * Mathf.PI;

  // ================== Methods

  protected override void EnableSeagullSpawner()
  {
    _currentAimDirection = getNormalizedDirectionToPlayer();
    InvokeRepeating("spawnScattershotSeagull", _spawnDelay, _spawnPeriod);
  }

  protected override void UpdateSeagullSpawner(float deltaTime)
  {
    _currentAimDirection = Vector3.RotateTowards(
      _currentAimDirection,
      getNormalizedDirectionToPlayer(),
      _radiansPerTime * deltaTime,
      0);
  }

  protected override void DisableSeagullSpawner() { CancelInvoke(); }

  // ================== Helpers

  // This spawns a seagull with a random initial velocity
  private void spawnScattershotSeagull()
  {
    SpawnSeagull(_rigidbody2D.position, scattershotUnitVector() * _spawnInitialSpeed);
  }
  
  // This returns a unit vector near-ish to the player's target direction
  private Vector2 scattershotUnitVector()
  {
    float random = RandomUtil.Triangular(-_spreadAngle, _spreadAngle) + getAimAngle();
    return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
  }

  private Vector2 getNormalizedDirectionToPlayer()
  {
    if (!PlayerManager.Instance)
    {
      float random = Random.Range(0.0f, 2.0f * Mathf.PI);
      return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }
    return (PlayerManager.Instance.PlayerPosition - _rigidbody2D.position).normalized;
  }

  private float getAimAngle()
  {
    return Mathf.Atan2(_currentAimDirection.y, _currentAimDirection.x);
  }
}
