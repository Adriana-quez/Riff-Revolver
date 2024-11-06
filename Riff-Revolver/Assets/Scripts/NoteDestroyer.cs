using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteDestroyer : MonoBehaviour
{
    public bool noteIsTouching;
    public KeyCode destroyKey = KeyCode.Space;
    private GameObject currentNote;
    private bool isDestroyed;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(destroyKey) && noteIsTouching)
        {
            GameManager.Instance.NoteHit();
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
        if (other.CompareTag("Note") && !isDestroyed)
        {
            noteIsTouching = false;
            currentNote = null;
            GameManager.Instance.NoteMissed();
        }
    }
}
