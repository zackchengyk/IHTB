using UnityEngine;

[DisallowMultipleComponent]
public class PlayerManagerScript : MonoBehaviour
{
  public static PlayerManagerScript Instance;
  
  [SerializeField] private GameObject _player;
  private Vector2 _lastPlayerLocation;

  // Accessor to protect _player from unauthorized access
  public GameObject PlayerGameObject { get { return _player; } }
  public ArrowHead  PlayerScript { get { return _player.GetComponent<ArrowHead>(); } }

  public bool PlayerIsAlive { get; set; }

  void Awake()
  {
    Instance = this;
    PlayerIsAlive = true;
  }

  public Vector2 GetPlayerPosition()
  {
    // Cache last location
    if (_player.GetComponent<Rigidbody2D>()) {
      _lastPlayerLocation = _player.GetComponent<Rigidbody2D>().position;
    }
    return _lastPlayerLocation;
  }
}
