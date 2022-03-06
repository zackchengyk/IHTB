using UnityEngine;

// Todo
public class CurvedBehaviour : SeagullBehaviour
{
  private float radiansPerTime = 2.0f;

  protected override void StartSeagullBehaviour()
  {
    this._seagull.GetComponentInChildren<Rigidbody2D>().velocity = this.initialVelocity;
    if (Random.value < 0.5) {
      radiansPerTime *= -1;
    }
  }

  protected override void UpdateSeagullBehaviour()
  {
    radiansPerTime = radiansPerTime * (1.0f - 0.5f * Time.deltaTime);

    this._seagull.GetComponentInChildren<Rigidbody2D>().velocity = 
      Rotate(this._seagull.GetComponentInChildren<Rigidbody2D>().velocity, Time.deltaTime * radiansPerTime);
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
