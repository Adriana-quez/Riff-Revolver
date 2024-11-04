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
        float sampledTimeInBeats = conductor.musicSource.timeSamples /
                                   (conductor.musicSource.clip.frequency * conductor.GetSecPerBeat(conductor.bpm));

        if (spawnIndex < spawnTimesInBeats.Length && sampledTimeInBeats >= spawnTimesInBeats[spawnIndex])
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
