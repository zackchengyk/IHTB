using UnityEngine;

// Todo
public class CurvedBehaviour : SeagullBehaviour
{
  private float radiansPerTime = 2.0f;

  protected override void StartSeagullBehaviour()
  {
    // Set initial velocity
    this.Velocity = this.initialVelocity;

    // Set rotation direction
    if (Random.value < 0.5) {
      this.radiansPerTime *= -1;
    }
  }

  protected override void UpdateSeagullBehaviour()
  {
    this.radiansPerTime = this.radiansPerTime * (1.0f - 0.5f * Time.deltaTime);

    // Set velocity
    Vector2 newDirection = Rotate(this.Velocity, Time.deltaTime * this.radiansPerTime);
    this.Velocity = newDirection;

    // Set transform rotation
    SetSpriteRotationToVec2(newDirection.normalized);
  }

  // Helper
  public static Vector2 Rotate(Vector2 v, float delta)
  {
    return new Vector2(
        v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
        v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
    );
  }
}
