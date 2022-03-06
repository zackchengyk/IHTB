using UnityEngine;

public abstract class SeagullBehaviour : MonoBehaviour
{
  // Things to be set when creating an instance of this prefab via Instantiate
  public GameObject _seagull;
  public Vector2 initialVelocity;

  // Start is called before the first frame update
  void Start()
  {
    Debug.Log("Seagull started!");
    this.StartSeagullBehaviour();
  }

  // Update is called once per frame
  void Update()
  {
    this.UpdateSeagullBehaviour();
  }

  // Must be overridden by concrete SeagullBehaviour
  protected abstract void StartSeagullBehaviour();
  protected abstract void UpdateSeagullBehaviour();
}
