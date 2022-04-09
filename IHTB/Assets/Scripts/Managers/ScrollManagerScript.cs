using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManagerScript : MonoBehaviour
{
  public static ScrollManagerScript Instance;

  [SerializeField] private Vector2 _scrollVelocity = new Vector3(0f, -1f);
  [SerializeField] private Material _backgroundScrollerMaterial;

  // Accessor
  public Vector2 ScrollVelocity
  {
    get {
      return _scrollVelocity;
    }
    set {
      _scrollVelocity = value;
      setBackgroundScrollSpeed();
    }
  }

  // Awake is called before the first frame update, whether enabled or not
  void Awake()
  {
    Instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    setBackgroundScrollSpeed();
  }

  // Update is called once per frame
  void Update()
  {
      
  }

  private void setBackgroundScrollSpeed()
  {
    Vector4 moveSpeed = _scrollVelocity * -200f / 3f;
    _backgroundScrollerMaterial.SetVector("_MoveSpeed", moveSpeed);
  }


  public double getScrollSpeed()
  {
    return _scrollVelocity.y; 
  }
}
