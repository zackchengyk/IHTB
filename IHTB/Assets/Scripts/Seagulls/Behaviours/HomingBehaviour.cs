using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBehaviour : SeagullBehaviour
{
  private const float _radiansPerTime = Mathf.PI / 4; // limit turning rate
  private const float _homingDuration = 2f;           // stop homing after some time
  public bool _isHoming;                              // stop homing after some time

  protected override void EnableSeagullBehaviour()
  {
    // Set defaults
    _isHoming = true;

    StartCoroutine("stopHomingAfterTime");
  }

  protected override void UpdateSeagullBehaviour()
  {
    // Get direction to player
    Vector2 direction = (PlayerManagerScript.Instance.GetPlayerPosition() - Position).normalized;

    // Set velocity
    if (_isHoming) Velocity = Vector3.RotateTowards(Velocity, direction, _radiansPerTime * Time.deltaTime, 0);
  }

  protected override void DisableSeagullBehaviour() { StopCoroutine("stopHomingAfterTime"); }

  // Helper
  private IEnumerator stopHomingAfterTime()
  {
    yield return new WaitForSeconds(_homingDuration);
    _isHoming = false;
  }
}

