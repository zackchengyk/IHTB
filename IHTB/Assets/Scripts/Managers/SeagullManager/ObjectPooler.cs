using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeagullIndex : int
{
  Linear = 0,
  Curved = 1,
  Sniper = 2,
  Homing = 3,
}

[System.Serializable]
public class ObjectPoolItem {
  public GameObject objectToPool;
  public int initialAmount;
}

public class ObjectPooler : MonoBehaviour {

	public static ObjectPooler Instance;

  [SerializeField] private List<ObjectPoolItem> _itemsToPool;

  private List<List<GameObject>> _pooledObjects;

	void Awake() {
		Instance = this;
	}

	// Start is called before the first frame update
  void Start () {

    _pooledObjects = new List<List<GameObject>>();
    
    // For each type of thing to pool
    for (int index = 0; index < _itemsToPool.Count; ++index)
    {
      ObjectPoolItem item = _itemsToPool[index];
      _pooledObjects.Add(new List<GameObject>());

      // Pre-instantiate some 
      for (int i = 0; i < item.initialAmount; ++i)
      {
        GameObject obj = Instantiate(item.objectToPool) as GameObject;
        obj.SetActive(false);
        _pooledObjects[index].Add(obj);
      }
    }
  }
	
  // Caller's responsibility to set active to true!
  public GameObject GetPooledObject(SeagullIndex seagullindex)
  {
    int index = (int)seagullindex;

    // Return if invalid index
    if (index >= _pooledObjects.Count) return null;

    // Check for existing instance
    for (int i = 0; i < _pooledObjects[index].Count; ++i)
    {
      if (!_pooledObjects[index][i].activeInHierarchy)
      {
        return _pooledObjects[index][i];
      }
    }

    // Otherwise, make a new one
    GameObject obj = Instantiate(_itemsToPool[index].objectToPool) as GameObject;
    obj.SetActive(false);
    _pooledObjects[index].Add(obj);
    return obj;
  }
}