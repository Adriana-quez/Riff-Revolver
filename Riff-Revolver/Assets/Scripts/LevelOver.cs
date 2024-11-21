using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI missesText;      
    public TextMeshProUGUI perfectsText;
    public TextMeshProUGUI greatsText;
    public TextMeshProUGUI goodsText;       
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI scoreText;

    public Image starImage;                 
    public Sprite[] starSprites;

    public Button replayButton;             
    public Button returnHomeButton;

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);

        replayButton.onClick.AddListener(ReplayGame);
        returnHomeButton.onClick.AddListener(ReturnToHome);
    }

    public void ShowGameOverPanel(int misses, int perfects, int greats, int goods, int combo, int score)
    {
        // Enable the panel
        gameOverPanel.SetActive(true);

        scoreText.text = "Score: " + $"{score}";
        missesText.text = $"{misses}";
        perfectsText.text = $"{perfects}";
        greatsText.text = $"{greats}";
        goodsText.text = $"{goods}";
        comboText.text = $"{combo}";

        int starIndex = CalculateStarRating(score);
        starImage.sprite = starSprites[starIndex];
    }

    // control amount of stars in menu
    private int CalculateStarRating(int score)
    {
        
        if (score >= 100) return 3; 
        if (score >= 75) return 2;  
        if (score >= 50) return 1;  
        return 0;                
    }

    private void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ReturnToHome()
    {
        SceneManager.LoadScene("Main Menu"); 
    }
}
