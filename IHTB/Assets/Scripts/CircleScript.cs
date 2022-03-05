using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
  [SerializeField] public Rigidbody2D circle;
  [SerializeField] public float speed = 5f;

  // Start is called before the first frame update
  void Start()
  {}

  // Update is called once per frame
  void Update()
  {
    circle.velocity = InputManagerScript.Instance.Movement * speed;
  }


    private void OnTriggerEnter2D(Collider2D other)
    {
        this.gameObject.SetActive(false);
    }
}
