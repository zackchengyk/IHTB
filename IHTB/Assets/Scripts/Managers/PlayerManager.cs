using UnityEngine;

[DisallowMultipleComponent]
public class PlayerManager : MonoBehaviour
{
  public static PlayerManager Instance;
  
  [SerializeField] private GameObject _player;

  private Player _playerScript;
  private Vector2 _lastPlayerLocation;

  // ================== Accessors

  public GameObject PlayerGameObject { get { return _player; } }

  public Vector2 PlayerPosition 
  {
    get {
      Rigidbody2D rigidbody = _player.GetComponent<Rigidbody2D>();
      if (rigidbody) _lastPlayerLocation = rigidbody.position;
      return _lastPlayerLocation;
    }
  }

  public Player PlayerScript { get { return _playerScript; } }
  
  public bool PlayerIsAlive { get; set; } = true;

  // ================== Methods

  void Awake()
  {
    Instance = this;
    _playerScript = _player.GetComponent<Player>();
  }
}
