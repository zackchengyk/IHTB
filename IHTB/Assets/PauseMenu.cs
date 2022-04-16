using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject resumeBtn;
    public GameObject backBtn;
    public GameObject quitBtn;
    // Start is called before the first frame update
    void Start()
    {
        //resumeBtn.onClick.addListener(this.resume());
    }

    public void doPause(bool pause)
    {
        pauseMenu.SetActive(pause);
    }

    public void Resume()
    {
        InputManager.Instance.Pausing = false;
    }

    

    // Update is called once per frame
    void Update()
    {
        if(InputManager.Instance.Pausing)
        {
            pauseMenu.SetActive(true);
            resumeBtn.SetActive(true);
            backBtn.SetActive(true);
            quitBtn.SetActive(true);

        }
        else
        {
            pauseMenu.SetActive(false);
            resumeBtn.SetActive(false);
            backBtn.SetActive(false);
            quitBtn.SetActive(false);

        }
    }
}
