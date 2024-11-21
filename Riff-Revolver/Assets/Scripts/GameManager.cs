using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentScore = 0;
    public int currentCombo = 0;
    public int highestCombo = 0;
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
    private bool isLevelOverPanelCalled;
    public Conductor conductor;
    public GameObject ScoringCanvas;
    private AccuracyText accuracy;
    public GameObject accuracyHolderPrefab;
    private GameObject accuracyInstance;

    void Start()
    {
        Instance = this;
        resetValues();
    }

    void Update()
    {
        if (currentCombo >= highestCombo) highestCombo = currentCombo;
        if (!conductor.getIsSongPlaying() && !isLevelOverPanelCalled) StartCoroutine(ShowResults());
    }

    IEnumerator ShowResults()
    {
        isLevelOverPanelCalled = true;
        Debug.Log("showing results...");
        levelOver.ShowGameOverPanel(misses, perfects, greats, goods, highestCombo, currentScore);
        yield return null;
    }

    private void resetValues()
    {
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
        currentScore = 0;
        currentCombo = 0;
        highestCombo = 0;
        perfects = 0;
        greats = 0;
        goods = 0;
        misses = 0;
        isLevelOverPanelCalled = false;
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

    public void GoodHit(Transform notePosition, string track)
    {
        Debug.Log("Good");
        goods++;
        currentCombo++;
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        showAccuracy("good", notePosition, track);
    }

    public void GreatHit(Transform notePosition, string track)
    {
        Debug.Log("Great");
        greats++;
        currentCombo++;
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        showAccuracy("great", notePosition, track);
    }

    public void PerfectHit(Transform notePosition, string track)
    {
        Debug.Log("Perfect");
        perfects++;
        currentCombo++;
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        showAccuracy("perfect", notePosition, track);
    }

    public void NoteMissed(Transform notePosition, string track)
    {
        Debug.Log("Miss");
        currentMultiplier = 1;
        multiplierTracker = 0;
        misses++;
        currentCombo = 0;

        multiplierText.text = "Multiplier: x" + currentMultiplier;
        showAccuracy("miss", notePosition, track);
    }

    public void showAccuracy(string state, Transform notePosition, string track)
    {
        switch (state)
        {
            case "perfect":
                spawnAndConfigureAccuracy("perfect", notePosition, track);
                break;
            case "great":
                spawnAndConfigureAccuracy("great", notePosition, track);
                break;
            case "good":
                spawnAndConfigureAccuracy("good", notePosition, track);
                break;
            case "miss":
                spawnAndConfigureAccuracy("miss", notePosition, track);
                break;
            default:
                spawnAndConfigureAccuracy("miss", notePosition, track);
                break;
        }
    }

    private void spawnAndConfigureAccuracy(string state, Transform notePosition, string track)
    {
        Vector2 accuracyTextSpawnPosition = new Vector2(0, 0);

        if (track == "Track1")
        {
            accuracyTextSpawnPosition = new Vector2(notePosition.position.x, notePosition.position.y);
        }
        else if (track == "Track2")
        {
            accuracyTextSpawnPosition = new Vector2(notePosition.position.x, notePosition.position.y);
        }

        accuracyInstance = Instantiate(accuracyHolderPrefab, accuracyTextSpawnPosition, Quaternion.identity);
        accuracy = accuracyInstance.transform.GetChild(0).gameObject.GetComponent<AccuracyText>();
        accuracy.ChangeAccuracyText(state);
    }
}
