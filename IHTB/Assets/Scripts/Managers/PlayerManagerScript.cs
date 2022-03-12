using UnityEngine;

[DisallowMultipleComponent]
public class PlayerManagerScript : MonoBehaviour
{
  public static PlayerManagerScript Instance;
  
  private GameObject _player;

  void Awake()
  {
    Instance = this;
    Debug.Log("Hello, world! PlayerManager awake.");
  }

  public Vector2 GetPlayerPosition()
  {
    return _player.transform.position;
  }
}
