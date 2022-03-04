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

    // Start is called before the first frame update
    void Start()
    {
        
    //Generates random movement direction for bullet
    RandomX = UnityEngine.Random.value;
    RandomY = UnityEngine.Random.value;

    //Movement Vector
    Movement = new Vector2(RandomX * speed * Time.deltaTime, RandomY * speed * Time.deltaTime);  
    }

    // Update is called once per frame
    void Update()
    {
        //Moves bullet based on the movement vector
        transform.Translate(Movement);
    }
}
