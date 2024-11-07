using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NoteDestroyer : MonoBehaviour
{
    public bool noteIsTouching;
    public KeyCode destroyKey = KeyCode.Space;
    private GameObject currentNote;
    private bool isDestroyed;

    private static float hitLineCenter;
    public float perfectTolerance;
    public float goodTolerance;
    public float normalTolerance;
    void Start()
    {
        hitLineCenter = transform.position.x;
        perfectTolerance = Mathf.Abs(0.03f * hitLineCenter);
        goodTolerance = Mathf.Abs(0.05f * hitLineCenter);
    }

    void Update()
    {
        if (Input.GetKeyDown(destroyKey) && noteIsTouching)
        {
            float currentNotePosFromCenter = Mathf.Abs(currentNote.transform.position.x - hitLineCenter);
            
            if (currentNotePosFromCenter <= perfectTolerance)
            {
                GameManager.Instance.PerfectHit();
            } else if (currentNotePosFromCenter > perfectTolerance && currentNotePosFromCenter <= goodTolerance)
            {
                GameManager.Instance.GoodHit();
            } else
            {
                GameManager.Instance.NormalHit();
            }

            isDestroyed = true;
            noteIsTouching = false;
            Destroy(currentNote);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            noteIsTouching = true;
            currentNote = other.gameObject;
            isDestroyed = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("is note destroyed: " + isDestroyed);
        if (other.CompareTag("Note") && !isDestroyed)
        {
            noteIsTouching = false;
            currentNote = null;
            GameManager.Instance.NoteMissed();
        }
    }
}
