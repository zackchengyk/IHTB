using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DebrisManager : MonoBehaviour
{
  public static DebrisManager Instance;

  private Vector2 _screenDimensions;
  private float _fractionOfScreenWidthThatIsBeach = 0.6f;
  private float _buffer = 0.5f;

  // ================== Methods

  void Awake() { Instance = this; }

  void Start()
  {
    _screenDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

    StartCoroutine(spawnDebris());
  }

  private IEnumerator spawnDebris()
  {
    // Spawn initial debris
    float defaultScrollSpeed = Mathf.Abs(ScrollManager.Instance.DefaultScrollVelocity.y);
    float currTime = selectWaitTime();
    float timeToScrollScreen = _screenDimensions.y * 2 / defaultScrollSpeed;
    while (currTime < timeToScrollScreen)
    {
      // Spawn a piece of debris
      PooledObjectIndex index = selectDebrisIndex();
      GameObject debris = ObjectPooler.Instance.GetPooledObject(index);
      Vector2 position = selectDebrisPosition(index);
      position.y = -_screenDimensions.y + currTime * defaultScrollSpeed;
      debris.GetComponent<Debris>().ResetWhenTakenFromPool(position, selectDebrisScale());
      debris.SetActive(true);

      currTime += selectWaitTime();
    }

    yield return new WaitForSeconds(currTime - timeToScrollScreen);

    // Continuously spawn debris
    while (PlayerManager.Instance.PlayerIsAlive)
    {
      // Spawn a piece of debris
      PooledObjectIndex index = selectDebrisIndex();
      GameObject debris = ObjectPooler.Instance.GetPooledObject(index);
      debris.GetComponent<Debris>().ResetWhenTakenFromPool(selectDebrisPosition(index), selectDebrisScale());
      debris.SetActive(true);

      // Wait between spawns
      yield return new WaitForSeconds(selectWaitTime());
    }
  }

  // ================== Helpers

  private PooledObjectIndex selectDebrisIndex()
  {
    return (PooledObjectIndex) Random.Range((int) PooledObjectIndex.PopsicleDebris, (int) PooledObjectIndex.SlushieDebris);
  }

  private Vector2 selectDebrisPosition(PooledObjectIndex index)
  {
    if (index == PooledObjectIndex.PopsicleDebris || index == PooledObjectIndex.SlushieDebris)
    {
      return selectBoardwalkPosition();
    }

    return selectBeachPosition();
  }

  private Vector2 selectBeachPosition() 
  {
    return new Vector2(
      _screenDimensions.x * Random.Range(-_fractionOfScreenWidthThatIsBeach, _fractionOfScreenWidthThatIsBeach),
      _screenDimensions.y + _buffer);
  }

  private Vector2 selectBoardwalkPosition()
  {
    return new Vector2(
      _screenDimensions.x * Random.Range(_fractionOfScreenWidthThatIsBeach, 1),
      _screenDimensions.y + _buffer);
  }

  private float selectDebrisScale()
  {
    return RandomUtil.Triangular(0.65f, 0.85f);
  }

  private float selectWaitTime()
  {
    return RandomUtil.Triangular(1f, 3f) / Mathf.Abs(ScrollManager.Instance.ScrollVelocity.y); 
  }
}