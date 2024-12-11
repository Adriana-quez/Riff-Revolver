using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenuButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void Play()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void Quit() {
        Application.Quit();
        /*if (EditorApplication.isPlaying) {
            EditorApplication.isPlaying = false;
        } else {
            Application.Quit();
        }*/
    }
}
