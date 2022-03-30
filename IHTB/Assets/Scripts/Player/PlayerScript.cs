using UnityEngine;

public class PlayerScript : MonoBehaviour
{
  [SerializeField] public Rigidbody2D circle;
  [SerializeField] public float speed = 5f;
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
    // If the game is paused, return
    if (InputManagerScript.Instance.Pausing) return;
    
    // Update velocity
    circle.velocity = InputManagerScript.Instance.Movement * speed;
  }

  // GetHit is called when a projectile hits the player
  public void GetHit(GameObject projectile)
  {
    // Make seagull take fry
    projectile.GetComponentInChildren<SeagullBehaviour>().FryTaken = !projectile.GetComponentInChildren<SeagullBehaviour>().FryTaken;

    // Decrement HP
    lives--;
    livesSystem.DisplayLives(lives, maxLives);

    // Die
    if (lives == 0) this.gameObject.SetActive(false);
  }
}
