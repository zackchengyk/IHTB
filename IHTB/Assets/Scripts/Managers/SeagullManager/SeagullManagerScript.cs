using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This spawns seagulls from the periphery of the screen
public class SeagullManagerScript : MonoBehaviour
{
  [SerializeField] protected GameObject _linearSeagull;
  
  private Vector2 _screenDimensions;
  private Vector2 _centerTopPosition;

  void Start()
  {
    _screenDimensions  = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    _centerTopPosition = new Vector2(0, _screenDimensions.y);

    StartCoroutine(spawnWalls());
  }

  void FixedUpdate() {}

  /* WALLS */

  [SerializeField] private Vector2 _wallVelocity = Vector2.down * 3.0f;

  private IEnumerator spawnWalls()
  {
    float spacing = 6;
    int offset = 3;
    while (PlayerManagerScript.Instance.PlayerIsAlive)
    {
      yield return new WaitForSeconds(Mathf.Abs(spacing / _wallVelocity.y));
      GenerateWall(spacing, offset);
      offset = offset == 3 ? 0 : 3;
    }
  }

  private void GenerateWall(float spacing, float offset)
  {
    int halfCount = (int)(_screenDimensions.x / spacing) + 1;

    for (int i = -halfCount; i <= halfCount; ++i)
    {
      SpawnSeagull(
        _linearSeagull,
        _centerTopPosition + Vector2.right * (spacing * i + offset),
        _wallVelocity);
    }
  }

  /* Spawning */

  private void SpawnSeagull(GameObject seagull, Vector3 position, Vector3 velocity)
  {
    GameObject obj = ObjectPooler.Instance.GetPooledObject(0);

    obj.GetComponent<SeagullBehaviour>().ResetWhenTakenFromPool(position, velocity);
    obj.SetActive(true);
  }
}