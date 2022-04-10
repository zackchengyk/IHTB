using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class InputManager : MonoBehaviour
{
  public static InputManager Instance;

	[HideInInspector] public Vector2 Movement  { get; private set; }
	[HideInInspector] public bool    DodgeRoll { get; private set; } = false;
	[HideInInspector] public bool    Pausing   { get; set;         } = false;

  void Awake() { Instance = this; }

  // Movement (WASD / arrows)
  public void OnMovementInput(InputAction.CallbackContext context)
  {
    Movement = context.ReadValue<Vector2>();
  }

  // Press/Hold to trigger dodge rolls
	public void onDodgeRollInput(InputAction.CallbackContext context)
  {
    if (context.started)
    {
      DodgeRoll = true;
    }
    else if (context.canceled)
    {
      DodgeRoll = false;
    }
  }

  // Press to toggle pause
	public void OnPauseInput(InputAction.CallbackContext context)
  {
    if (context.started)
    { 
      Pausing = !Pausing;
      Time.timeScale = Pausing ? 0 : 1;
    }
  }
}