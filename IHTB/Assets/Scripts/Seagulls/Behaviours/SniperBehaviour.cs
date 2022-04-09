using System.Collections;
using UnityEngine;

public class SniperBehaviour : SeagullBehaviour
{
  private const float _radiansPerTime = Mathf.PI / 4; // limit turning rate
  private int _phase;                                 // keep track of current phase
  private float _doubleSpeed;                         // keep track of 2 x initial speed
  private Vector2 _rotationDirection;                 // keep track of rotation as a direction vector 

  protected override void EnableSeagullBehaviour()
  {
    // Calculate and store _doubleSpeed
    _doubleSpeed = Velocity.magnitude * 2;

    // Start coroutine
    StartCoroutine("updatePhase");
  }

  protected override void UpdateSeagullBehaviour()
  {
    if (_phase == 1)
    {
      // Fix position
      Velocity = Vector2.zero;

      // Rotate towards player
      _rotationDirection = Vector3.RotateTowards(
        _rotationDirection,
        getNormalizedDirectionToPlayer(),
        _radiansPerTime,
        0);
      SetSpriteRotation(_rotationDirection);
    }
  }

  protected override void DisableSeagullBehaviour() { StopCoroutine("updatePhase"); }

  // ================== Helpers

  private IEnumerator updatePhase()
  {
    // First phase: fly in initial velocity
    _phase = 0;

    yield return new WaitForSeconds(1.0f);
    
    // Second phase: fix position and match rotation to face player
    _phase = 1;
    _rotationDirection = getNormalizedDirectionToPlayer();

    yield return new WaitForSeconds(1.5f);

    // Third phase: fly in a straight line towards players last-known location
    _phase = 2;
    Velocity = _rotationDirection * _doubleSpeed;
  }

  private Vector2 getNormalizedDirectionToPlayer()
  {
    return (PlayerManagerScript.Instance.GetPlayerPosition() - Position).normalized;
  }
}
