using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButtons : MonoBehaviour
{
    public GameObject modeMenu;
    public GameObject levelsMenu;
    public static bool level2Unlocked = false;
    public static bool level3Unlocked = false;
    public Button twoButton;
    public Button threeButton;
    [SerializeField] public Button oneButton;
    void Start()
    {
        
    }

    public void ShowLevels()
    {
        modeMenu.SetActive(false);
        levelsMenu.SetActive(true);
        twoButton.interactable = true;
        threeButton.interactable = true;

        oneButton.onClick.AddListener(changeButtonOneRouteLevel);
        twoButton.onClick.AddListener(changeButtonTwoRouteLevel);
        threeButton.onClick.AddListener(changeButtonThreeRouteLevel);
    }

    public void ShowCampaign()
    {
        modeMenu.SetActive(false);
        levelsMenu.SetActive(true);
        twoButton.interactable = true;
        threeButton.interactable = true;

        oneButton.onClick.AddListener(changeButtonOneRouteIntro);
        twoButton.onClick.AddListener(changeButtonTwoRouteIntro);
        threeButton.onClick.AddListener(changeButtonThreeRouteIntro);

        /*if (!level2Unlocked) 
        {
            twoButton.interactable = false;
            threeButton.interactable = false;
        }
        else if (!level3Unlocked)
        {
            twoButton.interactable = true;
            threeButton.interactable = false;
        }
        else {
            twoButton.interactable = true;
            threeButton.interactable = true;
        }*/
    }

    public void BackToModeMenu(){

        modeMenu.SetActive(true);
        levelsMenu.SetActive(false);
    }

    void changeButtonOneRouteIntro() {
        SceneManager.LoadScene("IntroCutscene");
    }

    void changeButtonOneRouteLevel() {
        SceneManager.LoadScene("DuelLevel");
    }

    void changeButtonTwoRouteIntro() {
        SceneManager.LoadScene("BarIntroScene");
    }

    void changeButtonTwoRouteLevel() {
        SceneManager.LoadScene("BarLevel");
    }

    void changeButtonThreeRouteIntro() {
        SceneManager.LoadScene("CherryIntroScene");
    }

    void changeButtonThreeRouteLevel() {
        SceneManager.LoadScene("CherrySpittingLevel");
    }
}
