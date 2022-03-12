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
  void Start()
  {
    Debug.Log("Hello, world! Circle started.");
  }

  // Update is called once per frame
  void Update()
  {
        //check if game is paused
        pausing = InputManagerScript.Instance.Pausing;
        Debug.Log("paused: " + pausing);
        // if the game is paused, set timescale to zero, and stop updating
        if (pausing)
        {
            Time.timeScale = 0f;
            return;

        }
        else
        {
            Time.timeScale = 1f;
        }
        circle.velocity = InputManagerScript.Instance.Movement * speed;
    if (InputManagerScript.Instance.FineMovement)
    {
      circle.velocity *= fineMovementModifier;
    }
  }


    private void OnTriggerEnter2D(Collider2D other)
    {
        this.gameObject.SetActive(false);
    }
}
