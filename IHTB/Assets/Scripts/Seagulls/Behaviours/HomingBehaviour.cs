using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBehaviour : SeagullBehaviour
{
  private const float _radiansPerTime = Mathf.PI / 3; // this limits the turning rate
  private const float _homingDuration = 2f;           // these stop the seagull's homing 
  public bool _isHoming;                              // behaviour after some time

  // ================== Methods

  protected override void EnableSeagullBehaviour() { StartCoroutine("stopHomingAfterTime"); }

  protected override void UpdateSeagullBehaviour()
  {
    if (_isHoming)
    {
      Velocity = Vector3.RotateTowards(
        Velocity,
        getNormalizedDirectionToPlayer(),
        _radiansPerTime * Time.deltaTime,
        0);
    }
  }

  protected override void DisableSeagullBehaviour() { StopCoroutine("stopHomingAfterTime"); }

  // ================== Helpers
  
  private IEnumerator stopHomingAfterTime()
  {
    _isHoming = true;
    yield return new WaitForSeconds(_homingDuration);
    _isHoming = false;
  }
}

