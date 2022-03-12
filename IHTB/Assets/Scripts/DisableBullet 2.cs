using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBullet : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManagerScript.Instance.Pausing) {
            Debug.Log("Timescale" + Time.timeScale);
            Time.timeScale = 0f;
        }
        else {
            Debug.Log("Timescale" + Time.timeScale);
            Time.timeScale = 1f;
        }
    }
}
