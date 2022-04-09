using UnityEngine;

public abstract class SeagullSpawner : MonoBehaviour
{
  public SeagullIndex Index; // the index of the type of seagull this spawner spawns

  private const float _defaultSpawnDelay  = 0.0f;
  private const float _defaultSpawnPeriod = 0.75f;
  private const float _defaultSpawnInitialSpeed = 3.5f;
  public float SpawnDelay;
  public float SpawnPeriod;
  public float SpawnInitialSpeed;

  protected Rigidbody2D _rigidbody2D;    // needed to scroll the spawner with the background

  void Awake() { _rigidbody2D = GetComponentInChildren<Rigidbody2D>(); }

  void OnEnable() { EnableSeagullSpawner(); }

  void FixedUpdate()
  {
    // Update velocity
    _rigidbody2D.velocity = ScrollManagerScript.Instance.ScrollVelocity;

    // Call the update function
    UpdateSeagullSpawner();
  }

  void OnDisable() { DisableSeagullSpawner(); }

  public void ResetWhenTakenFromPool(
    Vector2 position,
    SeagullIndex index,
    float spawnDelay        = _defaultSpawnDelay,
    float spawnPeriod       = _defaultSpawnPeriod,
    float spawnInitialSpeed = _defaultSpawnInitialSpeed)
  {
    transform.position = position;
    Index              = index;
    SpawnDelay         = spawnDelay;
    SpawnPeriod        = spawnPeriod;
    SpawnInitialSpeed  = spawnInitialSpeed;
  }

  // Must be overridden by concrete SeagullSpawner
  protected abstract void EnableSeagullSpawner();
  protected abstract void UpdateSeagullSpawner();
  protected abstract void DisableSeagullSpawner();

  // Helper
  protected void SpawnSeagull(Vector3 position, Vector3 velocity)
  {
    GameObject obj = ObjectPooler.Instance.GetPooledObject(Index);

    obj.GetComponent<SeagullBehaviour>().ResetWhenTakenFromPool(position, velocity);
    obj.SetActive(true);
  }
}
