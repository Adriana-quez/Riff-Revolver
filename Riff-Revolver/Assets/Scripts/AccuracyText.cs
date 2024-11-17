using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AccuracyText : MonoBehaviour
{
    public TextMeshProUGUI accuracyText;  
    public float changeInterval = 2f;     

    // RGB values for each state (normalized to 0-1 range)
    private Color perfectColor = new Color(1f, 0f, 1f);   
    private Color goodColor = new Color(0f, 1f, 0f);      
    private Color missColor = new Color(1f, 0f, 0f);     
    private int currentIndex = 0; 

    void Start()
    {
        // Start the repeating change of text and color
        InvokeRepeating("ChangeAccuracyText", 0f, changeInterval);
    }

    // Method to change the text and color
    void ChangeAccuracyText()
    {
        
        switch (currentIndex)
        {
            case 0: 
                accuracyText.text = "Perfect!";
                accuracyText.color = perfectColor;
                break;
            case 1: 
                accuracyText.text = "Good!";
                accuracyText.color = goodColor;
                break;
            case 2: 
                accuracyText.text = "Miss!";
                accuracyText.color = missColor;
                break;
        }

        // Move to the next state (loop back to 0 if at the end)
        currentIndex = (currentIndex + 1) % 3; 
    }
}
