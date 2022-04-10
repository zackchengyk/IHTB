using System.Collections;
using UnityEngine;

public class SniperBehaviour : SeagullBehaviour
{
  private const float _radiansPerTime = Mathf.PI / 4; // this limits the turning rate
  private int _phase;                                 // this keeps track of the current phase
  private float _doubleSpeed;                         // this stores 2 x initial speed for the final phase
  private Vector2 _rotationDirection;                 // this keep track of the rotation, as a direction vector 

  // ================== Methods

  protected override void EnableSeagullBehaviour() { StartCoroutine("updatePhase"); }

  protected override void UpdateSeagullBehaviour()
  {
    if (_phase == 1)
    {
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
    // First phase: follow initial velocity
    _phase = 0;
    _doubleSpeed = Velocity.magnitude * 2;

    yield return new WaitForSeconds(1.0f);
    
    // Second phase: fix position and match rotation to face player
    _phase = 1;
    Velocity = Vector2.zero;

    yield return new WaitForSeconds(1.5f);

    // Third phase: fly in a straight line towards players last-known location
    _phase = 2;
    Velocity = getNormalizedDirectionToPlayer() * _doubleSpeed;
  }
}
