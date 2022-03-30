using UnityEngine;

public abstract class SeagullSpawner : MonoBehaviour
{
  // Things to be set when creating the prefab corresponding to the concrete SeagullSpawner
  [SerializeField] protected GameObject _seagull; 
  [SerializeField] protected GameObject _spawner;

  // Start is called before the first frame update
  void Start()
  {
    this.StartSeagullSpawner();
  }

  // Update is called once per frame
  void Update()
  {
    // Update velocity
    this._spawner.GetComponentInChildren<Rigidbody2D>().velocity = ScrollManagerScript.Instance.ScrollVelocity;

    // Call the child's update function
    this.UpdateSeagullSpawner();
  }

  // Must be overridden by concrete SeagullSpawner
  protected abstract void StartSeagullSpawner();
  protected abstract void UpdateSeagullSpawner();
}
