using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitManager : MonoBehaviour
{
    public string sceneName;
    private bool change = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            if (!change)
            {
                change = true;
                changeScene();
            }
        }
    }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
