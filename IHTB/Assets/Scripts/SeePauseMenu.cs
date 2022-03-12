using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeePauseMenu : MonoBehaviour
{
    private static bool isPaused;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isPaused = InputManagerScript.Instance.Pausing;
        pauseMenu.SetActive(isPaused);
    }
}
