using UnityEngine;

public abstract class SeagullSpawner : MonoBehaviour
{
  // Things to be set when creating the prefab corresponding to the concrete SeagullSpawner
  [SerializeField] protected GameObject _seagull; 
  [SerializeField] protected GameObject _spawner;

  // TODO: make some kind of scroll manager which provides this
  protected Vector2 scrollVelocity = new Vector2(0, -1);

  // Start is called before the first frame update
  void Start()
  {
    Debug.Log("SeagullSpawner started!");
    this.StartSeagullSpawner();

    // TODO: there has to be a better way to do this (make the spawner scroll downward)
    this._spawner.GetComponentInChildren<Rigidbody2D>().velocity = scrollVelocity;
  }

  // Update is called once per frame
  void Update()
  {
    this.UpdateSeagullSpawner();
  }

  // Must be overridden by concrete SeagullSpawner
  protected abstract void StartSeagullSpawner();
  protected abstract void UpdateSeagullSpawner();
}
