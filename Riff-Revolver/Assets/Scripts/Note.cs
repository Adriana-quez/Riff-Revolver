using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int trackNumber;
    private GameObject hitLine;
    private NoteDestroyer noteDestroyer;
    private float hitLineCenter;
    public KeyCode destroyKey;
    public bool touching;
    public bool scored;
    void Start()
    {
        hitLine = GameObject.FindWithTag("HitLine");
        hitLineCenter = hitLine.transform.position.x;
        destroyKey = KeyCode.Space;
        touching = false;
        scored = false;
        noteDestroyer = hitLine.GetComponent<NoteDestroyer>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(destroyKey) && touching)
        {
            float currentNotePosFromCenter = Mathf.Abs(transform.position.x - hitLineCenter);

            if (currentNotePosFromCenter <= noteDestroyer.getPerfectTolerance())
            {
                GameManager.Instance.PerfectHit();
            }
            else if (currentNotePosFromCenter > noteDestroyer.getPerfectTolerance() && currentNotePosFromCenter <= noteDestroyer.getGoodTolerance())
            {
                GameManager.Instance.GoodHit();
            }
            else
            {
                GameManager.Instance.NormalHit();
            }

            scored = true;
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        if (!scored) GameManager.Instance.NoteMissed();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitLine"))
        {
            touching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("Note not touching hit line...");
        // Debug.Log("Note after trigger exit: " + scored);
        if (other.CompareTag("HitLine"))
        {
            touching = false;
            if (!scored)
            {
                GameManager.Instance.NoteMissed();
            }
        }
    }
}
