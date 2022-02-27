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
}
