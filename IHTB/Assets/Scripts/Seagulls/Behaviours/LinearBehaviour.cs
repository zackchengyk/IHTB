using UnityEngine;

public class LinearBehaviour : SeagullBehaviour
{
  protected override void StartSeagullBehaviour()
  {
    this._seagull.GetComponentInChildren<Rigidbody2D>().velocity = this.initialVelocity;
  }

  protected override void UpdateSeagullBehaviour() {}
}
