using UnityEngine;

// Todo
public class CurvedBehaviour : SeagullBehaviour
{
  // Keep track of turning rate
  private float _radiansPerTime = Mathf.PI * 3 / 4;

  protected override void EnableSeagullBehaviour()
  {
    // Select future rotation direction
    if (Random.value < 0.5)
    {
      _radiansPerTime *= -1;
    }
  }

  protected override void UpdateSeagullBehaviour()
  {
    // Decrease rotation with time
    _radiansPerTime = _radiansPerTime * (1.0f - 0.5f * Time.deltaTime);

    // Set velocity
    Velocity = rotateCCW(Velocity, Time.deltaTime * _radiansPerTime);
  }

  // Helper
  private Vector2 rotateCCW(Vector2 v, float delta)
  {
    return new Vector2(
        v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
        v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
    );
  }
}
