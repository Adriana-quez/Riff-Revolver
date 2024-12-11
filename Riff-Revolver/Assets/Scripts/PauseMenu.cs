using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)){
            Pause();
        }
    }

    //Methods for buttons
    public void Pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart(){
        GameManager.Instance.resetValues();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadLevels(){
        SceneManager.LoadScene("LevelSelect");
    }

    public void Quit()
    {
        Application.Quit();
        /*if (EditorApplication.isPlaying) {
            EditorApplication.isPlaying = false;
        } else {
            Application.Quit();
        }*/
    }

}