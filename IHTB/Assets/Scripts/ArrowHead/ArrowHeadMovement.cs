using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHeadMovement : MonoBehaviour
{
  private Rigidbody2D _arrowHeadRB;
  private Animator _animator;

  [SerializeField] private float _speed = 5f;

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
    if (InputManagerScript.Instance.Pausing) {
      Time.timeScale = 0f;
      return;
    }

    // If game is not paused, set timescale to 1
    Time.timeScale = 1f;
    
    // Update velocity
    _arrowHeadRB.velocity = InputManagerScript.Instance.Movement * _speed;

    // Update animation state(s)
    updateAnimationStates();
  }

  private void updateAnimationStates()
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
}
