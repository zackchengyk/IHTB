using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesSystem : MonoBehaviour
{
    [SerializeField] GameObject alivePrefab;
    [SerializeField] GameObject deadPrefab;

    public void DisplayLives(int lives, int maxLives)
    {
        //delete all current lives
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        //display new hearts
        for (int i = 0; i < maxLives; i++)
        {
            if (i < lives)
            {
                GameObject life = Instantiate(alivePrefab, transform.position, Quaternion.identity);
                life.transform.parent = transform;
            }
            else
            {
                GameObject life = Instantiate(deadPrefab, transform.position, Quaternion.identity);
                life.transform.parent = transform;
            }
        }
    }
}
