using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSeagull : Seagull
{
  [SerializeField] private float _speed = 2f;

  protected override void UpdateOwnPosition() {
    this._seagull.GetComponent<Rigidbody2D>().velocity = new Vector2(_speed, 0);
  }
}
