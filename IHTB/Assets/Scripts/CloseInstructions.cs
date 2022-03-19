using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseInstructions : MonoBehaviour
{
    public Image shade;
    public Image instructions;
    public Button x_out;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideInstructions()
    {
        shade.gameObject.SetActive(false);
        instructions.gameObject.SetActive(false);
        x_out.gameObject.SetActive(false);
    }
}
