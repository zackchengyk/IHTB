using UnityEngine;

[DisallowMultipleComponent]
public class PlayerManager : MonoBehaviour
{
  public static PlayerManager Instance;
  
  [SerializeField] private GameObject _player;
  private Vector2 _lastPlayerLocation;

  // ================== Accessors

  public GameObject PlayerGameObject { get { return _player; } }

  public ArrowHead PlayerScript
  {
    get { return _player.GetComponent<ArrowHead>(); }
  }

  public Vector2 PlayerPosition 
  {
    get {
      Rigidbody2D rigidbody = _player.GetComponent<Rigidbody2D>();
      if (rigidbody) _lastPlayerLocation = rigidbody.position;
      return _lastPlayerLocation;
    }
  }
  
  public bool PlayerIsAlive { get; set; } = true;

  // ================== Methods

  void Awake() { Instance = this; }
}
