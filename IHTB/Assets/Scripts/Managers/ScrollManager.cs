using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ScrollManager : MonoBehaviour
{
  public static ScrollManager Instance;

  [SerializeField] private Vector2 _scrollVelocity = new Vector3(0f, -1f);
  [SerializeField] private Material _backgroundScrollerMaterial;

  // ================== Accessors

  public Vector2 ScrollVelocity
  {
    get { return _scrollVelocity; }
    set {
      _scrollVelocity = value;
      setBackgroundScrollSpeed();
    }
  }

  // ================== Methods

  void Awake() { Instance = this; }

  void Start() { setBackgroundScrollSpeed(); }

  public double getScrollSpeed() { return _scrollVelocity.y;  }

  // ================== Helpers

  private void setBackgroundScrollSpeed()
  {
    Vector4 moveSpeed = _scrollVelocity * -200f / 3f; // magic numbers
    _backgroundScrollerMaterial.SetVector("_MoveSpeed", moveSpeed);
  }
}
