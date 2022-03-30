using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class InputManagerScript : MonoBehaviour
{
  public static InputManagerScript Instance;

	[HideInInspector] public Vector2 Movement;
	[HideInInspector] public bool Pausing = false;
	[HideInInspector] public bool DodgeRoll = false;
	// [HideInInspector] public bool FineMovement = false;

  void Awake()
  {
    Instance = this;
  }

  // Movement (WASD / arrows)
  public void OnMovementInput(InputAction.CallbackContext context)
  {
    Movement = context.ReadValue<Vector2>();
  }

  // Press to toggle pause
	public void OnPauseInput(InputAction.CallbackContext context)
  {
    if (context.started)
    { 
      Pausing = !Pausing; 
    }
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

  // // Hold to toggle fine movement
	// public void onFineModifierInput(InputAction.CallbackContext context)
  // {
  //   if (context.started)
  //   {
  //     FineMovement = true;
  //   }
  //   else if (context.canceled)
  //   {
  //     FineMovement = false;
  //   }
  // }
}