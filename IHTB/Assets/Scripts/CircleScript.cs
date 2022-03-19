using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
  [SerializeField] public Rigidbody2D circle;
  [SerializeField] public float speed = 5f;
  [SerializeField] public float fineMovementModifier = 0.5f;
  private static bool pausing;

  // Start is called before the first frame update
  void Start() {}

  // Update is called once per frame
  void Update()
  {
    // Check if game is paused
    pausing = InputManagerScript.Instance.Pausing;

    // If the game is paused, set timescale to zero, and stop updating
    if (pausing) {
      Time.timeScale = 0f;
      return;
    }

    // If game is not paused, set timescale to 1
    Time.timeScale = 1f;
    
    // Update velocity
    circle.velocity = InputManagerScript.Instance.Movement * speed;
    if (InputManagerScript.Instance.FineMovement)
    {
      circle.velocity *= fineMovementModifier;
    }
  }

  // GetHit is called when a projectile hits the player
  public void GetHit(GameObject projectile)
  {
    // Make seagull take fry
    Debug.Log("Player has been hit!");
    projectile.GetComponentInChildren<SeagullBehaviour>().FryTaken = !projectile.GetComponentInChildren<SeagullBehaviour>().FryTaken;
  }
}
