using UnityEngine;

[DisallowMultipleComponent]
public class PlayerManagerScript : MonoBehaviour
{
  public static PlayerManagerScript Instance;
  
  [SerializeField] public GameObject Player;

  void Awake()
  {
    Instance = this;
    Debug.Log("Hello, world! PlayerManager awake.");
  }

  public Vector2 GetPlayerPosition()
  {
    return Player.GetComponentInChildren<Rigidbody2D>().position;
  }
}
