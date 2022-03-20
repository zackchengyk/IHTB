using System.Collections;
using UnityEngine;

public class SniperBehaviour : SeagullBehaviour
{
  private int _phase = 0;
  private float _doubleSpeed = 0.0f;

  // TODO: there has to be a better way to swap between screen-space to world-space velocities...
  private Vector2 scrollVelocity = new Vector2(0, -1);

  protected override void StartSeagullBehaviour()
  {
    // Set velocity
    this.Velocity = this.initialVelocity;
    this._doubleSpeed = (this.initialVelocity - this.scrollVelocity).magnitude * 2;

    // Set transform rotation
    SetSpriteRotationToVec2(this.initialVelocity.normalized);

    // Start coroutine
    StartCoroutine(UpdatePhase());
  }

  protected override void UpdateSeagullBehaviour()
  {
    if (_phase == 1) {
      // Fix position and match rotation to face player
      this.Velocity = Vector2.zero;
      SetSpriteRotationToVec2((PlayerManagerScript.Instance.GetPlayerPosition() - this.Position).normalized);
    }
  }

  IEnumerator UpdatePhase()
  {
    // First phase: fly in initial velocity
    this._phase = 0;

    // Wait
    yield return new WaitForSeconds(1.0f);
    
    // Second phase: fix position and match rotation to face player
    this._phase = 1;

    // Wait
    yield return new WaitForSeconds(1.5f);

    // Third phase:  fly in a straight line towards players last-known location
    this._phase = 2;
    this.Velocity = (PlayerManagerScript.Instance.GetPlayerPosition() - this.Position).normalized * _doubleSpeed;
  }
}
