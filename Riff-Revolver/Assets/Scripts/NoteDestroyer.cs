using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteDestroyer : MonoBehaviour
{
    private static float hitLineCenter;
    public float perfectTolerance;
    public float greatTolerance;
    void Start()
    {
        hitLineCenter = transform.position.x;
        perfectTolerance = Mathf.Abs(0.03f * hitLineCenter);
        greatTolerance = Mathf.Abs(0.05f * hitLineCenter);
    }

    void Update()
    {
        
    }

    public float getPerfectTolerance() { return perfectTolerance; }
    public float getGreatTolerance() { return greatTolerance; }
}
