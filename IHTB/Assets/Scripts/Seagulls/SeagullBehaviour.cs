using UnityEngine;

public abstract class SeagullBehaviour : MonoBehaviour
{
  private Rigidbody2D _rigidbody2D;
  private Animator    _animator;
  private Vector2     _initialVelocity; // this is necessary because Rigidbody2D properties
                                        // can't be set until after the object is enabled;
                                        // however, _initialPosition is not necessary because the
                                        // Rigidbody2D acquires its position from transform.position
  
  // ================== Accessors

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

  // ================== Methods
  
  void Awake()
  {
    _rigidbody2D = GetComponentInChildren<Rigidbody2D>();
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

  // Must be overridden by concrete SeagullBehaviour
  protected abstract void EnableSeagullBehaviour();
  protected abstract void UpdateSeagullBehaviour();
  protected abstract void DisableSeagullBehaviour();

  // ================== Helpers

  protected void SetSpriteRotation(Vector2 direction)
  {
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    _rigidbody2D.rotation = angle;
  }

  protected Vector2 getNormalizedDirectionToPlayer()
  {
    return (PlayerManager.Instance.PlayerPosition - Position).normalized;
  }
}
