using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
  private Rigidbody2D _rigidbody2D;

  void Awake() { _rigidbody2D = GetComponent<Rigidbody2D>(); }

  void FixedUpdate() { _rigidbody2D.velocity = ScrollManager.Instance.ScrollVelocity; }

  public void ResetWhenTakenFromPool(Vector2 position, float scale)
  {
    transform.position = position;
    transform.localScale = new Vector3(scale, scale, scale);
    transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
  }
}
