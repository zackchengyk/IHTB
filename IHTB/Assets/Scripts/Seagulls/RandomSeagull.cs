using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TO DELETE

public class RandomSeagull : Seagull
{
  [SerializeField] private Vector2 _vector;

    void Awake()
    {
        _vector = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        _vector.Normalize();
        _vector *= 2; 
    }
  protected override void UpdateOwnPosition() {
    this._seagull.GetComponent<Rigidbody2D>().velocity = _vector;
  }
}
