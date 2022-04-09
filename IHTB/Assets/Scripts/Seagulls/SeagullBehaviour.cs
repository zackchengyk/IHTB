using UnityEngine;

public abstract class SeagullBehaviour : MonoBehaviour
{
  // Things that are internal
  private Rigidbody2D _rigidbody2D;
  private Animator _animator;
  private Vector2 _initialVelocity;
  
  // Accessors
  public bool FryTaken
  {
    get { return _animator.GetBool("FryTaken"); }
    set { _animator.SetBool("FryTaken", value); }
  }
  public Vector2 Position
  {
    get { return _rigidbody2D.position; }
    set { _rigidbody2D.position = value; }
  }
  public Vector2 Velocity
  {
    get { return _rigidbody2D.velocity; }
    set {
      _rigidbody2D.velocity = value;
      if (value != Vector2.zero) SetSpriteRotation(value.normalized);
    }
  }

  void Awake()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
    _animator    = GetComponentInChildren<Animator>();
  }

  void OnEnable()
  {
    Velocity = _initialVelocity;
    EnableSeagullBehaviour();
  }

  void FixedUpdate() { UpdateSeagullBehaviour(); }

  void onDisable() { DisableSeagullBehaviour(); }

  public void ResetWhenTakenFromPool(Vector2 position, Vector2 velocity)
  {
    transform.position = position;
    _initialVelocity = velocity;
  }

  // On trigger stay, check if other collider is player's and if so, call player's GetHit() method
  void OnTriggerStay2D(Collider2D other)
  {
    if (other.CompareTag("Player")) PlayerManagerScript.Instance.PlayerScript.GetHit(gameObject);
  }

  // Must be overridden by concrete SeagullBehaviour
  protected abstract void EnableSeagullBehaviour();
  protected abstract void UpdateSeagullBehaviour();
  protected abstract void DisableSeagullBehaviour();

  // Helper
  protected void SetSpriteRotation(Vector2 direction)
  {
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    _rigidbody2D.rotation = angle;
  }
}
