using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;

public class RandomBullet : MonoBehaviour
{
    [SerializeField]Vector2 Movement;
    [SerializeField]float RandomX;
    [SerializeField]float RandomY;
    [SerializeField]float speed;
    [SerializeField] static public List<int> bulletList;

    // declares pausing as static 
    private static Boolean pausing;

    // Start is called before the first frame update
    void Start()
    {
    //Creates a list to store disabled bullets
       bulletList = new List<int>();
    
    //Generates random movement direction for bullet
    RandomX = UnityEngine.Random.value;
    RandomY = UnityEngine.Random.value;

    //Movement Vector
    Movement = new Vector2(RandomX * speed * Time.deltaTime, RandomY * speed * Time.deltaTime);  
    


    }

    // Update is called once per frame
    void Update()
    {
        //check if game is paused
       pausing = InputManagerScript.Instance.Pausing;
        // if the game is paused, set timescale to zero, and stop updating
        if(pausing)
        {
            Time.timeScale = 0f;
            return;

        }
     
        //Moves bullet based on the movement vector
        transform.Translate(Movement);
        
        //Deactivates Bullet when it goes offscreen and adds it to the list
        if (transform.position.x > 12 || transform.position.x < -12 || transform.position.y < -6 || transform.position.y > 6) {
            gameObject.SetActive(false);
            bulletList.Add(1); 
        }

        if (bulletList[0] == 1) {
            gameObject.SetActive(true); //Renables Objects
            Movement = new Vector2(2f,3f);
            transform.position = Movement; //Resets the X and Y position
            Movement = new Vector2(RandomX * speed * Time.deltaTime, RandomY * speed * Time.deltaTime);  
            transform.Translate(Movement);
        }
        
   
    }
}
