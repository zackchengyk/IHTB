using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHead : MonoBehaviour
{
  private Rigidbody2D _rigidbody2D;
  private Animator _animator;
  private bool _isRolling = false;
  private int  _rolledThroughCount = 0;
  private bool _canRoll = true;
  private bool _isDead = false;
  private bool _hasBeenHitThisTick = false;

  [SerializeField] private float _speed = 4f;
  [SerializeField] private float _rollSpeedMultiplier = 1.7f;
  [SerializeField] private float _rollDuration = 0.4f;
  [SerializeField] private float _rollCooldown = 0.2f;

  [SerializeField] private int _currentHP = 3;
  [SerializeField] private int _maxHP = 3;
  [SerializeField] private LivesSystem _livesSystem;

  [SerializeField] private float   _hitShakeDurationRealtime = 1f;
  [SerializeField] private float   _hitTimeScale = 1f;
  [SerializeField] private float   _hitInvulnerabilityPeriodRealtime = 1.5f;
  [SerializeField] private float   _hitShakeFrequencyScale = 15f;
  [SerializeField] private Vector2 _hitShakeAmplitudeScale = Vector2.one / 4;

  [SerializeField] private float _deathDuration = 1f;

  // ================== Methods

  void Awake()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
    _animator = GetComponentInChildren<Animator>();
  }

  void Start()
  {
    _livesSystem.DisplayLives(_currentHP, _maxHP);
  }

  void FixedUpdate()
  {
    // Reset this
    _hasBeenHitThisTick = false;

    // If the game is paused, or the player is rolling or dead, do not update
    if (InputManager.Instance.Pausing || _isRolling || _isDead) return;

    // Start roll if requested
    if (_canRoll &&
        InputManager.Instance.DodgeRoll && 
        InputManager.Instance.Movement.sqrMagnitude > 0)
    {
      StartCoroutine(roll());
      return;
    }
    
    // Update velocity
    _rigidbody2D.velocity = InputManager.Instance.Movement * _speed;

    // Update running animation state(s)
    updateRunningAnimationStates();
  }

  // On trigger enter, increase combo if the other collider is a projectile and the player IS rolling
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("ProjectileWide") && _isRolling) _rolledThroughCount += 1;
  }

  // On trigger stay, get hit if the other collider is a projectile and the player IS NOT rolling
  void OnTriggerStay2D(Collider2D other)
  {
    if (other.CompareTag("Projectile") && !_isRolling && !_hasBeenHitThisTick) 
    {
      _hasBeenHitThisTick = true;
      GetHit(other.transform.root.gameObject);
      ScoreManager.Instance.DownMultiplier();
    }
  }

  // This is called when a projectile hits the player
  public void GetHit(GameObject projectile)
  {
    // If the player is dead, do not get hit
    if (_isDead) return;

    // Make seagull take fry
    projectile.GetComponent<SeagullBehaviour>().FryTaken = true;

    // Decrement HP
    _currentHP--;
    _livesSystem.DisplayLives(_currentHP, _maxHP);

    // Shake screen
    ShakerManager.Instance.ShakeOnceNonLinearRealtime(
      _hitShakeDurationRealtime,
      _hitShakeFrequencyScale,
      _hitShakeAmplitudeScale);

    // Die
    if (_currentHP == 0)
    {
      StartCoroutine(die());
      return;
    }
    
    // Slow time temporarily
    TimeManager.Instance.SlowTimeNonLinearRealtime(_hitTimeScale, _hitShakeDurationRealtime);

    // Create invulnerability period (may be less or more than shake duration)
    StopCoroutine (startInvulnerabilityPeriodRealtime());
    StartCoroutine(startInvulnerabilityPeriodRealtime());
  }

  // ================== Helpers

  // This is used in FixedUpdate
  private void updateRunningAnimationStates()
  {
    // Set isRunning
    bool isRunning = _rigidbody2D.velocity.sqrMagnitude > 0;
    _animator.SetBool("isRunning", isRunning);

    // Don't bother with direction if not running
    if (!isRunning) return;

    // Set runningDirection
    Vector2 vec = _rigidbody2D.velocity.normalized;
    float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
    if      (-135 <= angle && angle <= -45) _animator.SetInteger("runningDirection", 0);
    else if ( -45 <  angle && angle <   45) _animator.SetInteger("runningDirection", 1);
    else if (  45 <= angle && angle <= 135) _animator.SetInteger("runningDirection", 2);
    else                                    _animator.SetInteger("runningDirection", 3);
  }

  // This executes a roll
  private IEnumerator roll()
  {
    _isRolling = true;
    _canRoll = false;
    _rolledThroughCount = 0;

    _animator.SetTrigger("rollTrigger");
    _animator.speed = 1 / _rollDuration;
    _rigidbody2D.velocity = InputManager.Instance.Movement * _rollSpeedMultiplier * _speed;

    yield return new WaitForSeconds(_rollDuration);

    _isRolling = false;

    _animator.speed = 1;
    ScoreManager.Instance.UpMultiplier(_rolledThroughCount);

    yield return new WaitForSeconds(_rollCooldown);

    _canRoll = true;
  }

  // This makes the player temporarily invulnerable
  private IEnumerator startInvulnerabilityPeriodRealtime()
  {
    // _invulnerable = true;
    Physics2D.IgnoreLayerCollision(7, 8, true);

    _animator.SetBool("invulnerable", true);

    yield return new WaitForSecondsRealtime(_hitInvulnerabilityPeriodRealtime);

    // _invulnerable = false;
    // if (!_isRolling) Physics2D.IgnoreLayerCollision(7, 8, false);
    Physics2D.IgnoreLayerCollision(7, 8, false);

    _animator.SetBool("invulnerable", false);
  }

  // This is called when the player dies
  private IEnumerator die()
  {
    _isDead = true;
    _rigidbody2D.velocity = Vector2.zero;

    PlayerManager.Instance.PlayerIsAlive = false;
    ScrollManager.Instance.ScrollVelocity = Vector2.zero;

    _animator.SetTrigger("deathTrigger");
    _animator.speed = 1 / _deathDuration;

    // Todo: particles

    yield return new WaitForSecondsRealtime(_deathDuration);

    _animator.speed = 1;

    // gameObject.SetActive(false);
    enabled = false;
  }
}
