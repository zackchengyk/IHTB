using UnityEngine;

[DisallowMultipleComponent]
public class PlayerManagerScript : MonoBehaviour
{
  public static PlayerManagerScript Instance;
  
  [SerializeField] private GameObject _player;
  private Vector2 _lastPlayerLocation;

  // Accessor to prevent _player from unauthorized access
  public GameObject PlayerGameObject
  {
    get { return _player; }
  }

  void Awake()
  {
    Instance = this;
  }

  public Vector2 GetPlayerPosition()
  {
    // Cache last location
    if (_player.GetComponentInChildren<Rigidbody2D>()) {
      _lastPlayerLocation = _player.GetComponentInChildren<Rigidbody2D>().position;
    }
    return _lastPlayerLocation;
  }
}
