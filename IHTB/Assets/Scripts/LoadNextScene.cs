using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    //[SerializeField] private HorizontalWipe horizontalWipe;
    //[SerializeField] private sth;

    public void StartNextScene() {
        StartCoroutine(nextScene());
    }


    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
