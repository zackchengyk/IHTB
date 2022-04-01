using UnityEngine;

// This corresponds to an actual thing that scrolls with the screen
public abstract class SeagullSpawner : MonoBehaviour
{
  // Things to be set when creating the prefab corresponding to the concrete SeagullSpawner
  [SerializeField] protected SeagullIndex _seagullindex;

  protected Rigidbody2D _rigidbody2D;

  void Awake() { _rigidbody2D = GetComponentInChildren<Rigidbody2D>(); }

  void OnEnable() { EnableSeagullSpawner(); }

  void Update()
  {
    // Update velocity
    _rigidbody2D.velocity = ScrollManagerScript.Instance.ScrollVelocity;

    // Call the update function
    UpdateSeagullSpawner();
  }

  void OnDisable() { DisableSeagullSpawner(); }

  // Must be overridden by concrete SeagullSpawner
  protected abstract void EnableSeagullSpawner();
  protected abstract void UpdateSeagullSpawner();
  protected abstract void DisableSeagullSpawner();

  // Helper
  protected void SpawnSeagull(Vector3 position, Vector3 velocity)
  {
    GameObject obj = ObjectPooler.Instance.GetPooledObject(_seagullindex);

    obj.GetComponent<SeagullBehaviour>().ResetWhenTakenFromPool(position, velocity);
    obj.SetActive(true);
  }
}
