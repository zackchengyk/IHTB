using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class InputManagerScript : MonoBehaviour
{
  public static InputManagerScript Instance;

	[HideInInspector] public Vector2 Movement;
	[HideInInspector] public bool Pausing = false;
	[HideInInspector] public bool FineMovement = false;

  void Awake()
  {
    Instance = this;
    Debug.Log("Hello, world! InputManager awake.");
  }

  // Movement (WASD / arrows)
  public void OnMovementInput(InputAction.CallbackContext context)
  {
    Movement = context.ReadValue<Vector2>();
    Debug.Log("movement = " + Movement.x + " " + Movement.y);
  }

  // Press to toggle pause 
	public void OnPauseInput(InputAction.CallbackContext context)
  {
    if (context.started)
    { 
      Pausing = !Pausing; 
      Debug.Log("paused = " + Pausing);
    }
  }

  // Hold to toggle fine movement
	public void onFineModifierInput(InputAction.CallbackContext context)
  {
    if (context.started)
    {
      FineMovement = true;
    }
    else if (context.canceled)
    {
      FineMovement = false;
    }
    Debug.Log("fine movement = " + FineMovement);
  }
}