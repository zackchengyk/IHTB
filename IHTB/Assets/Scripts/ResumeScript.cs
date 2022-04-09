using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void resume()
    {
        InputManagerScript.Instance.Pausing = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
