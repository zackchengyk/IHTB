using UnityEngine;

public abstract class SeagullSpawner : MonoBehaviour
{
  private const float _defaultSpawnDelay  = 0.0f;
  private const float _defaultSpawnPeriod = 0.75f;
  private const float _defaultSpawnInitialSpeed = 3.5f;

  protected SeagullIndex _seagullIndex; // the index of the type of seagull this spawner spawns
  protected float _spawnDelay;
  protected float _spawnPeriod;
  protected float _spawnInitialSpeed;

  protected Rigidbody2D _rigidbody2D; // this allows the spawner to scroll with the background

  // ================== Methods

  void Awake() { _rigidbody2D = GetComponentInChildren<Rigidbody2D>(); }

  void OnEnable() { EnableSeagullSpawner(); }

  void FixedUpdate()
  {
    _rigidbody2D.velocity = ScrollManager.Instance.ScrollVelocity;
    UpdateSeagullSpawner();
  }

  void OnDisable() { DisableSeagullSpawner(); }

  public void ResetWhenTakenFromPool(
    Vector2 position,
    SeagullIndex seagullIndex,
    float spawnDelay        = _defaultSpawnDelay,
    float spawnPeriod       = _defaultSpawnPeriod,
    float spawnInitialSpeed = _defaultSpawnInitialSpeed)
  {
    transform.position = position;
    _seagullIndex      = seagullIndex;
    _spawnDelay        = spawnDelay;
    _spawnPeriod       = spawnPeriod;
    _spawnInitialSpeed = spawnInitialSpeed;
  }

  // Must be overridden by concrete SeagullSpawner
  protected abstract void EnableSeagullSpawner();
  protected abstract void UpdateSeagullSpawner();
  protected abstract void DisableSeagullSpawner();

  // ================== Helpers
  
  protected void SpawnSeagull(Vector3 position, Vector3 velocity)
  {
    GameObject obj = ObjectPooler.Instance.GetPooledObject(_seagullIndex);
    obj.GetComponent<SeagullBehaviour>().ResetWhenTakenFromPool(position, velocity);
    obj.SetActive(true);
  }
}
