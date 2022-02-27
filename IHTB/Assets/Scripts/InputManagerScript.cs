using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class InputManagerScript : MonoBehaviour
{
  public static InputManagerScript Instance;

	[HideInInspector] public Vector2 Movement;
	[HideInInspector] public bool Pausing = false;

  void Awake()
  {
    Instance = this;
    Debug.Log("Hello, world!");
  }

  public void OnMovementInput(InputAction.CallbackContext context)
  {
    Movement = context.ReadValue<Vector2>();
    Debug.Log("movement = " + Movement.x + " " + Movement.y);
  }

	public void OnPauseInput(InputAction.CallbackContext context)
  {
    if (context.started)
    { 
      Pausing = !Pausing; 
      Debug.Log("paused = " + Pausing);
    }
  }
}