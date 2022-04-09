using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagerScript : MonoBehaviour
{

    double score; 
    ScrollManagerScript script;
    GameObject scroll; 

    // Start is called before the first frame update
    void Start()
    {

        score = 0.0;

        scroll = GameObject.Find("ScrollManager");

        script = scroll.GetComponent<ScrollManagerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        score = score - script.getScrollSpeed() * Time.deltaTime;

        Debug.Log("Your Score: " + (int)score);
    }
}
