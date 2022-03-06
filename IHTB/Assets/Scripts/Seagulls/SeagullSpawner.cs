using UnityEngine;

public abstract class SeagullSpawner : MonoBehaviour
{
  // Things to be set when creating the prefab corresponding to the concrete SeagullSpawner
  [SerializeField] protected GameObject _seagull; 
  [SerializeField] protected GameObject _spawner;

  // Todo: make some kind of scroll manager which provides this
  protected Vector2 scrollVelocity = new Vector2(0, -1);

  // Start is called before the first frame update (protected virtual to allow overriding)
  protected virtual void Start()
  {
    Debug.Log("Seagull spawner started!");

    // Make spawner scroll downward
    this._spawner.GetComponentInChildren<Rigidbody2D>().velocity = scrollVelocity;
  }
}
