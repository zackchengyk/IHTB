using UnityEngine;

public abstract class SeagullSpawner : MonoBehaviour
{
  private const float _defaultSpawnDelay  = 0.0f;
  private const float _defaultSpawnPeriod = 1f;
  private const float _defaultSpawnInitialSpeed = 4f;

  protected PooledObjectIndex _seagullIndex; // the index of the type of seagull this spawner spawns
  protected float _spawnDelay;
  protected float _spawnPeriod;
  protected float _spawnInitialSpeed;

  protected Rigidbody2D    _rigidbody2D; // this allows the spawner to scroll with the background
  protected SpriteRenderer _spriteRenderer;
  private   Color          _originalColor;

  // ================== Methods

  void Awake()
  {
    _rigidbody2D    = GetComponentInChildren<Rigidbody2D>();
    _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    _originalColor  = _spriteRenderer.color;
  }

  void OnEnable() { EnableSeagullSpawner(); }

  void FixedUpdate()
  {
    _rigidbody2D.velocity = ScrollManager.Instance.ScrollVelocity;
    UpdateSeagullSpawner(Time.deltaTime);
  }

  void OnDisable() { DisableSeagullSpawner(); }

  public void ResetWhenTakenFromPool(
    Vector2 position,
    PooledObjectIndex seagullIndex,
    float spawnDelay        = _defaultSpawnDelay,
    float spawnPeriod       = _defaultSpawnPeriod,
    float spawnInitialSpeed = _defaultSpawnInitialSpeed)
  {
    transform.position    = position;
    _seagullIndex         = seagullIndex;
    _spawnDelay           = spawnDelay;
    _spawnPeriod          = spawnPeriod;
    _spawnInitialSpeed    = spawnInitialSpeed;
    _spriteRenderer.color = _originalColor;

    switch (_seagullIndex) {
      case PooledObjectIndex.Homing:
      {
        _spawnPeriod       *= 2;
        _spawnInitialSpeed /= 1.5f;
        _spriteRenderer.color = Color.red;
        break;
      }
    }
  }

  // Must be overridden by concrete SeagullSpawner
  protected abstract void EnableSeagullSpawner();
  protected abstract void UpdateSeagullSpawner(float deltaTime);
  protected abstract void DisableSeagullSpawner();

  // ================== Helpers
  
  protected void SpawnSeagull(Vector3 position, Vector3 velocity)
  {
    GameObject obj = ObjectPooler.Instance.GetPooledObject(_seagullIndex);
    obj.GetComponentInChildren<SeagullBehaviour>().ResetWhenTakenFromPool(position, velocity);
    obj.SetActive(true);
  }
}
