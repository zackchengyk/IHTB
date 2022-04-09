using UnityEngine;

public abstract class SeagullBehaviour : MonoBehaviour
{
  // Things that are internal
  private Rigidbody2D _rigidbody2D;
  private Animator _animator;
  private Vector2 _initialVelocity;
  
  // Accessors
  public bool FryTaken {
    get { return _animator.GetBool("FryTaken"); }
    set { _animator.SetBool("FryTaken", value); }
  }
  public Vector2 Position {
    get { return _rigidbody2D.position; }
    set { _rigidbody2D.position = value; }
  }
  public Vector2 Velocity {
    get { return _rigidbody2D.velocity; }
    set {
      _rigidbody2D.velocity = value;
      if (value != Vector2.zero) SetSpriteRotation(value.normalized);
    }
  }

  void Awake()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
    Debug.Log(_rigidbody2D);
    _animator = GetComponentInChildren<Animator>();
  }

  void OnEnable() {
    Velocity = _initialVelocity;
    EnableSeagullBehaviour();
  }

  void FixedUpdate() { UpdateSeagullBehaviour(); }

  public void ResetWhenTakenFromPool(Vector2 position, Vector2 velocity)
  {
    transform.position = position;
    _initialVelocity = velocity;
  }

  // Must be overridden by concrete SeagullBehaviour
  protected abstract void EnableSeagullBehaviour();
  protected abstract void UpdateSeagullBehaviour();

  // Helper
  protected void SetSpriteRotation(Vector2 direction)
  {
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    _rigidbody2D.rotation = angle;
  }

  // On trigger stay, check if other collider is player's and if so, call player's GetHit() method
  private void OnTriggerStay2D(Collider2D other)
  {
    // If collider is not player's, return
    if (!other.CompareTag("Player")) return;

    // Call player's GetHit() method
    PlayerManagerScript.Instance.PlayerScript.GetHit(gameObject);
  }
}

// // To delete: check if player is within some distance, and if so, toggle approach animation
// Vector2 separation = PlayerManagerScript.Instance.GetPlayerPosition() - Position;
// _animator.SetBool("Approach", separation.magnitude < 2);
