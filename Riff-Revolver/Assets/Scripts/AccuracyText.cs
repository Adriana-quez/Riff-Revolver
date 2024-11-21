using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AccuracyText : MonoBehaviour
{
    public TextMeshProUGUI accuracyText;

    // RGB values for each state (normalized to 0-1 range)
    private Color goodColor = new Color(1f, 0f, 1f);   
    private Color greatColor = new Color(0f, 1f, 0f);
    private Color perfectColor = new Color(0f, 1f, 1f);
    private Color missColor = new Color(1f, 0f, 0f);

    // Method to change the text and color
    public void ChangeAccuracyText(string state)
    {
        switch (state)
        {
            case "perfect": 
                accuracyText.text = "Perfect!";
                accuracyText.color = perfectColor;
                break;
            case "great": 
                accuracyText.text = "Great!";
                accuracyText.color = greatColor;
                break;
            case "good": 
                accuracyText.text = "Good!";
                accuracyText.color = goodColor;
                break;
            case "miss":
                accuracyText.text = "Miss!";
                accuracyText.color = missColor;
                break;
            default:
                accuracyText.text = "Miss!";
                accuracyText.color = missColor;
                break;
        }
    }

    public void DestroyParent()
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        Destroy(parent);
    }
}
