using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteDestroyer : MonoBehaviour
{
    private static float hitLineCenter;
    public float perfectTolerance;
    public float goodTolerance;
    void Start()
    {
        hitLineCenter = transform.position.x;
        perfectTolerance = Mathf.Abs(0.03f * hitLineCenter);
        goodTolerance = Mathf.Abs(0.05f * hitLineCenter);
    }

    void Update()
    {
        
    }

    public float getPerfectTolerance() { return perfectTolerance; }
    public float getGoodTolerance() { return goodTolerance; }
}
