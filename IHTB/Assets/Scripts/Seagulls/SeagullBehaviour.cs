using UnityEngine;

public abstract class SeagullBehaviour : MonoBehaviour
{
  // Things to be set when creating an instance of this prefab via Instantiate
  public GameObject _seagull;
  public Vector2 initialVelocity;

  // Start is called before the first frame update
  void Start()
  {
    this.StartSeagullBehaviour();
  }

  // Update is called once per frame
  void Update()
  {
    this.UpdateSeagullBehaviour();
  }

  // Must be overridden by concrete SeagullBehaviour
  protected abstract void StartSeagullBehaviour();
  protected abstract void UpdateSeagullBehaviour();

  // Helpers
  protected void SetSpriteRotationToVec2(Vector2 vec) {
    float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
    this._seagull.GetComponentInChildren<Rigidbody2D>().rotation = angle;
  }

  // Check if other is player
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.transform.IsChildOf(PlayerManagerScript.Instance.PlayerGameObject.transform) ) {
      PlayerManagerScript.Instance.PlayerGameObject.GetComponentInChildren<CircleScript>().GetHit(this.gameObject);
    }
  }
}
