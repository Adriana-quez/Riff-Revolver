using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentScore = 0;
    public int currentCombo = 0;
    public int perfects = 0;
    public int greats = 0;
    public int goods = 0;
    public int misses = 0;

    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public TMP_Text scoreText;
    public TMP_Text multiplierText;

    public LevelOver levelOver;
    public Conductor conductor;

    void Start()
    {
        Instance = this;
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
    }

    void Update()
    {
        if (!conductor.getIsSongPlaying())
        {
            StartCoroutine("ShowResults");
        }
    }

    IEnumerable ShowResults()
    {
        yield return new WaitForSeconds(3f);
        levelOver.ShowGameOverPanel(misses, perfects, greats, goods, currentCombo, currentScore);
    }

    public void NoteHit()
    {
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiplierText.text = "Multiplier: x" + currentMultiplier.ToString();
        scoreText.text = "Score: " + currentScore.ToString();
    }

    public void GoodHit()
    {
        Debug.Log("Good");
        goods++;
        currentCombo++;
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
    }

    public void GreatHit()
    {
        Debug.Log("Great");
        greats++;
        currentCombo++;
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
    }

    public void PerfectHit()
    {
        Debug.Log("Perfect");
        perfects++;
        currentCombo++;
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
    }

    public void NoteMissed()
    {
        Debug.Log("Miss");
        currentMultiplier = 1;
        multiplierTracker = 0;
        misses++;
        currentCombo = 0;

        multiplierText.text = "Multiplier: x" + currentMultiplier;
    }
}
