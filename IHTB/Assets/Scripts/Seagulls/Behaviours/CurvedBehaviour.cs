using UnityEngine;

// Todo
public class CurvedBehaviour : SeagullBehaviour
{
  private const float _initialRadiansPerTime = Mathf.PI * 3 / 4; // this limits the turning rate;
  private float _radiansPerTime;                                 // it will be decreased with time

  // ================== Methods

  protected override void EnableSeagullBehaviour()
  {
    // Set initial turning rate
    _radiansPerTime = _initialRadiansPerTime;

    // Select future turning direction
    if (Random.value < 0.5) _radiansPerTime *= -1;
  }

  protected override void UpdateSeagullBehaviour()
  {
    // Decrease turning rate with time
    _radiansPerTime = _radiansPerTime * (1.0f - 0.5f * Time.deltaTime);

    // Set velocity
    Velocity = rotateCCW(Velocity, Time.deltaTime * _radiansPerTime);
  }

  protected override void DisableSeagullBehaviour() {}

  // ================== Helpers

  private Vector2 rotateCCW(Vector2 v, float delta)
  {
    return new Vector2(
        v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
        v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
    );
  }
}
