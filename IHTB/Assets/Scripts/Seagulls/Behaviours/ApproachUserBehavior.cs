using UnityEngine;

public class ApproachUserBehavior : SeagullBehaviour
{
    private Vector2 playerPosition = PlayerManagerScript.Instance.GetPlayerPosition();
    private Vector2 seagullPosition;

    protected override void StartSeagullBehaviour()
    {
        seagullPosition = this.transform.position;
        Vector2 direction = playerPosition - seagullPosition;
        Debug.Log("playerPosition" + playerPosition);
        Debug.Log("direction.x: " + direction.x + " direction.y: " + direction.y);

        // Set velocity
        this._seagull.GetComponentInChildren<Rigidbody2D>().velocity = direction;

        // Set transform rotation
        SetSpriteRotationToVec2(this.initialVelocity.normalized);
    }

    protected override void UpdateSeagullBehaviour() { }
}

