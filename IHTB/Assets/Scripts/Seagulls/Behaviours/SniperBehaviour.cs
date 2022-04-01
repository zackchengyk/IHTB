using System.Collections;
using UnityEngine;

public class SniperBehaviour : SeagullBehaviour
{
  // Limit turning rate
  private const float _radiansPerTime = Mathf.PI / 4;

  private int _phase = 0;
  private float _doubleSpeed;
  private Vector2 _previousDirection;

  protected override void EnableSeagullBehaviour()
  {
    // Calculate and store _doubleSpeed
    _doubleSpeed = Velocity.magnitude * 2;

    // Start coroutine
    StartCoroutine(UpdatePhase());
  }

  protected override void UpdateSeagullBehaviour()
  {
    if (_phase == 1)
    {
      // Fix position
      Velocity = Vector2.zero;

      // Rotate towards player
      _previousDirection = Vector3.RotateTowards(_previousDirection, getNormalizedDirectionToPlayer(), _radiansPerTime, 0);
      SetSpriteRotation(_previousDirection);
    }
  }

  private IEnumerator UpdatePhase()
  {
    // First phase: fly in initial velocity
    _phase = 0;

    yield return new WaitForSeconds(1.0f);
    
    // Second phase: fix position and match rotation to face player
    _phase = 1;
    _previousDirection = getNormalizedDirectionToPlayer();

    yield return new WaitForSeconds(1.5f);

    // Third phase: fly in a straight line towards players last-known location
    _phase = 2;
    Velocity = _previousDirection * _doubleSpeed;
  }

  private Vector2 getNormalizedDirectionToPlayer()
  {
    return (PlayerManagerScript.Instance.GetPlayerPosition() - Position).normalized;
  }
}
