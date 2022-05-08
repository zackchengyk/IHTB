using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DebrisManager : MonoBehaviour
{
  public static DebrisManager Instance;

  private Vector2 _screenDimensions;
  private float _fractionOfScreenWidthThatIsBeach = 0.6f;
  private float _yBuffer = 1f;

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
    float timeToScrollScreen = (_yBuffer + _screenDimensions.y * 2) / defaultScrollSpeed;
    while (currTime < timeToScrollScreen)
    {
      // Spawn a piece of debris
      PooledObjectIndex index = selectDebrisIndex();
      GameObject debris = ObjectPooler.Instance.GetPooledObject(index);
      Vector2 modifiedPosition = selectDebrisPosition(index);
      modifiedPosition.y = -_screenDimensions.y - _yBuffer + currTime * defaultScrollSpeed;
      debris.GetComponent<Debris>().ResetWhenTakenFromPool(
        modifiedPosition,
        selectDebrisScale   (index),
        selectDebrisRotation(index));
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
      debris.GetComponent<Debris>().ResetWhenTakenFromPool(
        selectDebrisPosition(index),
        selectDebrisScale   (index),
        selectDebrisRotation(index));
      debris.SetActive(true);

      // Wait between spawns
      yield return new WaitForSeconds(selectWaitTime());
    }
  }

  // ================== Helpers

  private PooledObjectIndex selectDebrisIndex()
  {
    // This is funny
    if (IHTBSceneManager.Instance.IsEasyGame) return PooledObjectIndex.UmbrellaDebris;

    return (PooledObjectIndex) Random.Range((int) PooledObjectIndex.PopsicleDebris, (int) PooledObjectIndex.BottleDebrisAgain + 1);
  }

  private Vector2 selectDebrisPosition(PooledObjectIndex index)
  {
    if (index == PooledObjectIndex.PopsicleDebris ||
      index == PooledObjectIndex.SlushieDebris ||
      index == PooledObjectIndex.BirdPoop1Debris ||
      index == PooledObjectIndex.BirdPoop2Debris ||
      index == PooledObjectIndex.DirtDebris)
    {
      return selectBoardwalkPosition();
    }

    if (index == PooledObjectIndex.TowelBlueDebris ||
      index == PooledObjectIndex.TowelCyanDebris ||
      index == PooledObjectIndex.TowelGreenDebris ||
      index == PooledObjectIndex.TowelPurpleDebris ||
      index == PooledObjectIndex.UmbrellaDebris)
    {
      return selectConstrainedBeachPosition();
    }

    return selectBeachPosition();
  }

  private Vector2 selectConstrainedBeachPosition() 
  {
    return new Vector2(
      _screenDimensions.x * Random.Range(-_fractionOfScreenWidthThatIsBeach + 0.2f, _fractionOfScreenWidthThatIsBeach - 0.1f),
      _screenDimensions.y + _yBuffer);
  }

  private Vector2 selectBeachPosition() 
  {
    return new Vector2(
      _screenDimensions.x * Random.Range(-_fractionOfScreenWidthThatIsBeach, _fractionOfScreenWidthThatIsBeach),
      _screenDimensions.y + _yBuffer);
  }

  private Vector2 selectBoardwalkPosition()
  {
    return new Vector2(
      _screenDimensions.x * Random.Range(_fractionOfScreenWidthThatIsBeach + 0.1f, 0.9f),
      _screenDimensions.y + _yBuffer);
  }

  private float selectDebrisScale(PooledObjectIndex index)
  {
    if (index == PooledObjectIndex.TowelBlueDebris ||
      index == PooledObjectIndex.TowelCyanDebris ||
      index == PooledObjectIndex.TowelGreenDebris ||
      index == PooledObjectIndex.TowelPurpleDebris ||
      index == PooledObjectIndex.UmbrellaDebris)
    {
      return 1;
    }

    return RandomUtil.Triangular(0.65f, 0.85f);
  }

  private float selectDebrisRotation(PooledObjectIndex index)
  {
    if (index == PooledObjectIndex.TowelBlueDebris ||
      index == PooledObjectIndex.TowelCyanDebris ||
      index == PooledObjectIndex.TowelGreenDebris ||
      index == PooledObjectIndex.TowelPurpleDebris ||
      index == PooledObjectIndex.UmbrellaDebris)
    {
      return 0;
    }

    return Random.Range(0, 360);
  }

  private float selectWaitTime()
  {
    return RandomUtil.Triangular(1f, 3f) / Mathf.Abs(ScrollManager.Instance.ScrollVelocity.y); 
  }
}