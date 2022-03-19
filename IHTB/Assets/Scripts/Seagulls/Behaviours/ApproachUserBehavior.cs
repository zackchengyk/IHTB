using UnityEngine;

public class ApproachUserBehavior : SeagullBehaviour
{
  [SerializeField] private float _speed = 2.5f;

  protected override void StartSeagullBehaviour()
  {
    Vector2 playerPosition = PlayerManagerScript.Instance.GetPlayerPosition();
    Vector2 seagullPosition = this.Position;
    Vector2 direction = (playerPosition - seagullPosition).normalized;
    
    this.initialVelocity = _speed * direction;

    // Set velocity
    this.Velocity = this.initialVelocity;

    // Set transform rotation
    SetSpriteRotationToVec2(this.initialVelocity.normalized);
  }

  protected override void UpdateSeagullBehaviour() {}
}

