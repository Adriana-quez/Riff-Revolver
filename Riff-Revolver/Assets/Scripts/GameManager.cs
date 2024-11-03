using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        
    }

    public void NoteHit()
    {
        Debug.Log("Hit");
    }

    public void NoteMissed()
    {
        Debug.Log("Miss");
    }
}
