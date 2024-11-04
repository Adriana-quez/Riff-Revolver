using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtons : MonoBehaviour
{
    public GameObject modeMenu;
    public GameObject levelsMenu;
    public static bool level2Unlocked = false;
    public static bool level3Unlocked = false;
    public Button twoButton;
    public Button threeButton;
    void Start()
    {
        
    }

    public void ShowLevels()
    {
        modeMenu.SetActive(false);
        levelsMenu.SetActive(true);
        twoButton.interactable = true;
        threeButton.interactable = true;
    }

    public void ShowCampaign()
    {
        modeMenu.SetActive(false);
        levelsMenu.SetActive(true);
        if (!level2Unlocked) 
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
        }
    }

    public void BackToModeMenu(){

        modeMenu.SetActive(true);
        levelsMenu.SetActive(false);
    }
}
