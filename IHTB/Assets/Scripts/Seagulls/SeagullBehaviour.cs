using UnityEngine;

public abstract class SeagullBehaviour : MonoBehaviour
{
  // Things to be set when creating an instance of this prefab via Instantiate
  [SerializeField] private GameObject seagull;
  public Vector2 initialVelocity;

  // Things that are internal
  private Rigidbody2D _rigidBody2D;
  private bool _fryTaken = false;
  
  // Accessors
  public bool FryTaken {
    get {
      return _fryTaken;
    }
    set {
      _fryTaken = value;
      seagull.GetComponent<Animator>().SetBool("FryTaken", _fryTaken);
    }
  }
  public Vector2 Position {
    get { return seagull.GetComponentInChildren<Rigidbody2D>().position; }
  }
  public Vector2 Velocity {
    get { return seagull.GetComponentInChildren<Rigidbody2D>().velocity; }
    set { seagull.GetComponentInChildren<Rigidbody2D>().velocity = value; }
  }
  public float Rotation {
    get { return seagull.GetComponentInChildren<Rigidbody2D>().rotation; }
    set { seagull.GetComponentInChildren<Rigidbody2D>().rotation = value; }
  }

  // Start is called before the first frame update
  void Start()
  {
    this.StartSeagullBehaviour();
  }

  // Update is called once per frame
  void Update()
  {
    this.UpdateSeagullBehaviour();

    // Check if player is within some distance, and if so, toggle approach animation
    Vector2 separation = PlayerManagerScript.Instance.GetPlayerPosition() - this.Position;
    seagull.GetComponent<Animator>().SetBool("Approach", separation.magnitude < 2);
  }

  // Must be overridden by concrete SeagullBehaviour
  protected abstract void StartSeagullBehaviour();
  protected abstract void UpdateSeagullBehaviour();

  // Helpers
  protected void SetSpriteRotationToVec2(Vector2 vec) {
    float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
    this.Rotation = angle;
  }

  // On trigger enter, check if other collider is player's and if so, call player's GetHit() method
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.transform.IsChildOf(PlayerManagerScript.Instance.PlayerGameObject.transform) ) {
      PlayerManagerScript.Instance.PlayerGameObject.GetComponentInChildren<CircleScript>().GetHit(this.gameObject);
    }
  }
}
