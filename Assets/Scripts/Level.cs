using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    [SerializeField] float delaySeconds = 2;

    public void LoadGameOver()
    {
        StartCoroutine(DelayScene());
    }

    IEnumerator DelayScene()
    {

        Debug.Log("Wait");
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
