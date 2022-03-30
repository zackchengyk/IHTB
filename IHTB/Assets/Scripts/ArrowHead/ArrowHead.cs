using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHead : MonoBehaviour
{
  private Rigidbody2D _arrowHeadRB;
  private Animator _animator;
  private bool _rolling = false;
  private bool _canRoll = true;
  private bool _invulnerable = false;

  [SerializeField] private float _speed = 3f;
  [SerializeField] private float _rollSpeedMultiplier = 2f;
  [SerializeField] private float _rollDuration = 0.4f;
  [SerializeField] private float _rollCooldown = 0.2f;

  [SerializeField] private int _currentHP = 3;
  [SerializeField] private int _maxHP = 3;
  [SerializeField] private LivesSystem _livesSystem;

  // Start is called before the first frame update
  void Start()
  {
    _arrowHeadRB = gameObject.GetComponent<Rigidbody2D>();
    _animator = gameObject.GetComponentInChildren<Animator>();

    _livesSystem.DisplayLives(_currentHP, _maxHP);
  }

  // Update is called once per frame
  void Update()
  {
    // If the game is paused, set timescale to zero, and do not update
    if (InputManagerScript.Instance.Pausing)
    {
      Time.timeScale = 0f;
      return;
    }

    // If game is not paused, set timescale to 1
    Time.timeScale = 1f;

    // Return if currently rolling
    if (_rolling) return;

    // Start roll if requested
    if (_canRoll &&
        InputManagerScript.Instance.DodgeRoll && 
        InputManagerScript.Instance.Movement.sqrMagnitude > 0)
    {
      StartCoroutine(executeRoll());
      return;
    }
    
    // Update velocity
    _arrowHeadRB.velocity = InputManagerScript.Instance.Movement * _speed;

    // Update running animation state(s)
    updateRunningAnimationStates();
  }

  // Helper
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

  // Coroutine to execute a roll
  private IEnumerator executeRoll()
  {
    _rolling = true;
    _canRoll = false;
    _invulnerable = true;
    _animator.SetTrigger("rollTrigger");
    _animator.speed = 1 / _rollDuration;
    _arrowHeadRB.velocity = InputManagerScript.Instance.Movement * _rollSpeedMultiplier * _speed;

    yield return new WaitForSeconds(_rollDuration);

    _rolling = false;
    _invulnerable = false;
    _animator.speed = 1;

    yield return new WaitForSeconds(_rollCooldown);

    _canRoll = true;
  }

  // GetHit is called when a projectile hits the player
  public void GetHit(GameObject projectile)
  {
    // If invulnerable, return
    if (_invulnerable) return;

    // Make seagull take fry
    projectile.GetComponentInChildren<SeagullBehaviour>().FryTaken = !projectile.GetComponentInChildren<SeagullBehaviour>().FryTaken;

    // Decrement HP
    _currentHP--;
    _livesSystem.DisplayLives(_currentHP, _maxHP);

    // Die
    if (_currentHP == 0) gameObject.SetActive(false);
  }
}
