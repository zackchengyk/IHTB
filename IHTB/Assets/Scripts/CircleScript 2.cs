using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
  [SerializeField] public Rigidbody2D circle;
  [SerializeField] public float speed = 5f;
  [SerializeField] public float fineMovementModifier = 0.5f;

  // Start is called before the first frame update
  void Start()
  {
    Debug.Log("Hello, world! Circle started.");
  }

  // Update is called once per frame
  void Update()
  {
    if (InputManagerScript.Instance.Pausing) {
      Debug.Log("returned, shoudlnt move");
      Time.timeScale = 0f;
      return; 
    }

    circle.velocity = InputManagerScript.Instance.Movement * speed;
    if (InputManagerScript.Instance.FineMovement)
    {
      circle.velocity *= fineMovementModifier;
    }
  }


    private void OnTriggerEnter2D(Collider2D other)
    {
      if(InputManagerScript.Instance.Pausing) {
      }
        this.gameObject.SetActive(false);
    }
}
