using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHeadMovement : MonoBehaviour
{
  private Rigidbody2D _arrowHeadRB;
  private Animator _animator;
  private bool _rolling = false;
  private bool _canRoll = true;

  [SerializeField] private float _speed = 3f;
  [SerializeField] private float _rollSpeedMultiplier = 2f;
  [SerializeField] private float _rollDuration = 0.4f;
  [SerializeField] private float _rollCooldown = 0.2f;

  // Start is called before the first frame update
  void Start()
  {
    _arrowHeadRB = this.gameObject.GetComponent<Rigidbody2D>();
    _animator = this.gameObject.GetComponentInChildren<Animator>();
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
        InputManagerScript.Instance.FineMovement && 
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

  private IEnumerator executeRoll()
  {
    _rolling = true;
    _canRoll = false;
    _animator.SetTrigger("rollTrigger");
    _animator.speed = 1 / _rollDuration;
    _arrowHeadRB.velocity = InputManagerScript.Instance.Movement * _rollSpeedMultiplier * _speed;

    yield return new WaitForSeconds(_rollDuration);

    _rolling = false;
    _animator.speed = 1;

    yield return new WaitForSeconds(_rollCooldown);

    _canRoll = true;
  }
}
