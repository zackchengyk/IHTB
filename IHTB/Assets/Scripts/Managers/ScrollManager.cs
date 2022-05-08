using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ScrollManager : MonoBehaviour
{
  public static ScrollManager Instance;

  [SerializeField] private Vector2 _defaultScrollVelocity = new Vector3(0f, -1.75f);
  [SerializeField] private Vector2 _scrollVelocity = new Vector3(0f, -1.75f);
  [SerializeField] private Material _backgroundScrollerMaterial;

  // ================== Accessors

  public Vector2 DefaultScrollVelocity { get { return _defaultScrollVelocity; } }
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

  // ================== Helpers

  private void setBackgroundScrollSpeed()
  {
    Vector4 moveSpeed = _scrollVelocity * -200f / 3f; // magic numbers
    _backgroundScrollerMaterial.SetVector("_MoveSpeed", moveSpeed);
  }
}
