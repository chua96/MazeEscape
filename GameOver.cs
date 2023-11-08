using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void OnQuit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void OnReplay()
    {
        SceneManager.LoadScene("Level - 1");
        Debug.Log("Load Scene");
    }
}
