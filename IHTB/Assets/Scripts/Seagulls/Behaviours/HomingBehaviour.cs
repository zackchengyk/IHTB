using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBehaviour : SeagullBehaviour
{
  // Limit turning rate, and set to zero after some time
  private float _radiansPerTime = Mathf.PI / 4;
  private float _homingDuration = 5f;

  protected override void EnableSeagullBehaviour()
  {
    StartCoroutine(StopHomingAfterTime());
  }

  protected override void UpdateSeagullBehaviour()
  {
    // Get direction to player
    Vector2 direction = (PlayerManagerScript.Instance.GetPlayerPosition() - Position).normalized;

    // Set velocity
    Velocity = Vector3.RotateTowards(Velocity, direction, _radiansPerTime * Time.deltaTime, 0);
  }

  private IEnumerator StopHomingAfterTime()
  {
    yield return new WaitForSeconds(_homingDuration);
    _radiansPerTime = 0;
  }
}

