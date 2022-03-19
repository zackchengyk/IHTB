using UnityEngine;

public class LinearBehaviour : SeagullBehaviour
{
  protected override void StartSeagullBehaviour()
  {
    // Set velocity
    this.Velocity = this.initialVelocity;

    // Set transform rotation
    SetSpriteRotationToVec2(this.initialVelocity.normalized);
  }

  protected override void UpdateSeagullBehaviour() {}
}
