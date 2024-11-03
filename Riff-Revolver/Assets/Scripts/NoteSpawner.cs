using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject noteObj;
    public Transform spawnPoint;
    public float[] spawnTimesInBeats;

    private int spawnIndex = 0;
    public Conductor conductor;
    void Start()
    {
        if (conductor == null)
        {
            Debug.LogError("Conductor not assigned in NoteSpawner!");
        }
    }

    void Update()
    {
        if (spawnIndex < spawnTimesInBeats.Length && conductor.songPositionInBeats >= spawnTimesInBeats[spawnIndex])
        {
            SpawnNote();
            spawnIndex++;
        }
    }

    public void SpawnNote()
    {
        Instantiate(noteObj, spawnPoint.position, Quaternion.identity);
    }
}
