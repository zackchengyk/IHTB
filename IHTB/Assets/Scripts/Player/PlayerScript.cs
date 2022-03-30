using UnityEngine;

public class PlayerScript : MonoBehaviour
{
  [SerializeField] public Rigidbody2D circle;
  [SerializeField] public float speed = 5f;
  [SerializeField] public float fineMovementModifier = 0.5f;
  private static bool pausing;
  private Vector3 initialPosition;

  private int lives = 3;
  private int maxLives = 3;
  [SerializeField] LivesSystem livesSystem;

  // Start is called before the first frame update
  void Start() {
        this.initialPosition = this.transform.position;
        Debug.Log("player position: " + this.initialPosition);

        this.livesSystem.DisplayLives(3, 3);
    }

  // Update is called once per frame
  void Update()
  {
    // Check if game is paused
    pausing = InputManagerScript.Instance.Pausing;

    // If the game is paused, set timescale to zero, and stop updating
    if (pausing) {
      Time.timeScale = 0f;
      return;
    }

    // If game is not paused, set timescale to 1
    Time.timeScale = 1f;
    
    // Update velocity
    circle.velocity = InputManagerScript.Instance.Movement * speed;
    if (InputManagerScript.Instance.FineMovement)
    {
      circle.velocity *= fineMovementModifier;
    }
  }

  // GetHit is called when a projectile hits the player
  public void GetHit(GameObject projectile)
  {
    // Make seagull take fry
    Debug.Log("Player has been hit!");
    projectile.GetComponentInChildren<SeagullBehaviour>().FryTaken = !projectile.GetComponentInChildren<SeagullBehaviour>().FryTaken;

    lives--;
    Debug.Log("Still has " + lives);
    livesSystem.DisplayLives(lives, maxLives);
    this.gameObject.transform.position = this.initialPosition;
    if (lives == 0)
    {
        this.gameObject.SetActive(false);
    }
  }
}
