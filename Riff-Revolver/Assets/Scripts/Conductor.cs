using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Conductor : MonoBehaviour
{
    [SerializeField] public float bpm;
    public float songPosition;
    public float songPositionInBeats;
    public float dspSongTime;
    public AudioSource musicSource;
    public float firstBeatOffset;
    [SerializeField] private Intervals[] _intervals;
    public bool isPlaying;
    private int activeNotes;
    public bool beatmapOver;
    
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        dspSongTime = (float)AudioSettings.dspTime;
        musicSource.Play();
        isPlaying = true;
        activeNotes = 0; 
        beatmapOver = false;
    }

    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
        songPositionInBeats = songPosition / GetSecPerBeat(bpm);

        foreach (Intervals interval in _intervals)
        {
            float sampledTime = (musicSource.timeSamples / (musicSource.clip.frequency * interval.GetSecPerBeatInterval(bpm)));
            interval.CheckForNewInterval(sampledTime);
        }

        if (!musicSource.isPlaying)
        {
            isPlaying = false;
        }
    }

    public float GetSecPerBeat(float bpm)
    {
        return 60f / bpm;
    }

    [System.Serializable]
    public class Intervals
    {
        [SerializeField] private float _steps;
        [SerializeField] private UnityEvent _trigger;
        private int _lastInterval;
        public float GetSecPerBeatInterval(float bpm)
        {
            return 60f / (bpm * _steps);
        }

        public void CheckForNewInterval(float interval)
        {
            if (Mathf.FloorToInt(interval) != _lastInterval)
            {
                _lastInterval = Mathf.FloorToInt(interval);
                _trigger.Invoke();
            }
        }
    }

    public void MarkBeatmapComplete()
    {
        beatmapOver = true;
    }

    public void IncrementActiveNotes()
    {
        activeNotes++;
    }

    public void DecrementActiveNotes()
    {
        activeNotes--;
    }

    public bool getIsSongPlaying()
    {
        return isPlaying;
    }

    public bool GetBeatMapOver()
    {
        return beatmapOver;
    }
}
