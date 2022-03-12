using UnityEngine;

[DisallowMultipleComponent]
public class PlayerManagerScript : MonoBehaviour
{
  public static PlayerManagerScript Instance;
  
  [SerializeField] private GameObject Player;
  private Vector2 _lastPlayerLocation;

  void Awake()
  {
    Instance = this;
    Debug.Log("Hello, world! PlayerManager awake.");
  }

  public Vector2 GetPlayerPosition()
  {
    if (Player.GetComponentInChildren<Rigidbody2D>()) {
      _lastPlayerLocation = Player.GetComponentInChildren<Rigidbody2D>().position;
    }
    return _lastPlayerLocation;
  }
}
