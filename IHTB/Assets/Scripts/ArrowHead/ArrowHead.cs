using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHead : MonoBehaviour
{
  private Rigidbody2D _arrowHeadRB;
  private Animator _animator;
  private bool _isRolling = false;
  private bool _canRoll = true;
  private bool _invulnerable = false;
  private bool _isDead = false;

  [SerializeField] private float _speed = 3.5f;
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

  // Start is called before the first frame update
  void Start()
  {
    _arrowHeadRB = gameObject.GetComponent<Rigidbody2D>();
    _animator = gameObject.GetComponentInChildren<Animator>();

    _livesSystem.DisplayLives(_currentHP, _maxHP);
  }

  // FixedUpdate may be called any number of times per frame
  void FixedUpdate()
  {
    // If the game is paused, or the player is rolling or dead, do not update
    if (InputManagerScript.Instance.Pausing || _isRolling || _isDead) return;

    // Start roll if requested
    if (_canRoll &&
        InputManagerScript.Instance.DodgeRoll && 
        InputManagerScript.Instance.Movement.sqrMagnitude > 0)
    {
      StartCoroutine(roll());
      return;
    }
    
    // Update velocity
    _arrowHeadRB.velocity = InputManagerScript.Instance.Movement * _speed;

    // Update running animation state(s)
    updateRunningAnimationStates();
  }

  // Helper for FixedUpdate
  private void updateRunningAnimationStates()
  {
    // Set isRunning
    bool isRunning = _arrowHeadRB.velocity.sqrMagnitude > 0;
    _animator.SetBool("isRunning", isRunning);

    // Don't bother with direction if not running
    if (!isRunning) return;

    // Set runningDirection
    Vector2 vec = _arrowHeadRB.velocity.normalized;
    float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
    if      (-135 <= angle && angle <= -45) _animator.SetInteger("runningDirection", 0);
    else if ( -45 <  angle && angle <   45) _animator.SetInteger("runningDirection", 1);
    else if (  45 <= angle && angle <= 135) _animator.SetInteger("runningDirection", 2);
    else                                    _animator.SetInteger("runningDirection", 3);
  }

  // GetHit is called when a projectile hits the player
  public void GetHit(GameObject projectile)
  {
    // If the player is dead, do not get hit
    if (_isDead) return;

    // Make seagull take fry
    projectile.GetComponentInChildren<SeagullBehaviour>().FryTaken = !projectile.GetComponentInChildren<SeagullBehaviour>().FryTaken;

    // Decrement HP
    _currentHP--;
    _livesSystem.DisplayLives(_currentHP, _maxHP);


    // Shake screen
    ShakerManagerScript.Instance.ShakeOnceNonLinearRealtime(
      _hitShakeDurationRealtime,
      _hitShakeFrequencyScale,
      _hitShakeAmplitudeScale);

    // Die
    if (_currentHP == 0)
    {
      StartCoroutine("die");
      return;
    }
    
    // Slow time temporarily
    TimeManagerScript.Instance.SlowTimeNonLinearRealtime(_hitTimeScale, _hitShakeDurationRealtime);

    // Create invulnerability period (may be less or more than shake duration)
    StopCoroutine("startInvulnerabilityPeriodRealtime");
    StartCoroutine("startInvulnerabilityPeriodRealtime");
  }

  /* Helper coroutines */

  // Execute a roll
  private IEnumerator roll()
  {
    _isRolling = true;
    Physics2D.IgnoreLayerCollision(7, 8, true);
    _canRoll = false;

    _animator.SetTrigger("rollTrigger");
    _animator.speed = 1 / _rollDuration;
    _arrowHeadRB.velocity = InputManagerScript.Instance.Movement * _rollSpeedMultiplier * _speed;

    yield return new WaitForSeconds(_rollDuration);

    _isRolling = false;
    if (!_invulnerable) Physics2D.IgnoreLayerCollision(7, 8, false);

    _animator.speed = 1;

    yield return new WaitForSeconds(_rollCooldown);

    _canRoll = true;
  }

  // Become temporarily invulnerable
  private IEnumerator startInvulnerabilityPeriodRealtime()
  {
    _invulnerable = true;
    Physics2D.IgnoreLayerCollision(7, 8, true);

    _animator.SetBool("invulnerable", true);

    yield return new WaitForSecondsRealtime(_hitInvulnerabilityPeriodRealtime);

    _invulnerable = false;
    if (!_isRolling) Physics2D.IgnoreLayerCollision(7, 8, false);

    _animator.SetBool("invulnerable", false);
  }

  // Todo: die
  private IEnumerator die()
  {
    _isDead = true;
    _arrowHeadRB.velocity = Vector2.zero;

    ScrollManagerScript.Instance.ScrollVelocity = Vector2.zero;

    _animator.SetTrigger("deathTrigger");
    _animator.speed = 1 / _deathDuration;

    // Todo: particles

    yield return new WaitForSecondsRealtime(_deathDuration);

    _animator.speed = 1;

    // gameObject.SetActive(false);
    enabled = false;
  }
}
