using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Note : MonoBehaviour
{
    public float moveSpeed = 2f;
    public string track;
    private GameObject hitLine;
    private NoteDestroyer noteDestroyer;
    private float hitLineCenter;
    public KeyCode[] destroyKeys;
    public bool touching;
    public bool scored;
    private SpriteRenderer spriteRenderer;
    public Sprite barUpSprite;
    public Sprite barDownSprite;
    public Conductor conductor;
    public bool isLastNote = false;
    private bool isFirstNoteHit = false;

    public Sprite bulletUpSprite;
    public Sprite bulletDownSprite;
    public Sprite bucketUpSprite;
    public Sprite bucketDownSprite;

    void Start()
    {
        hitLine = GameObject.FindWithTag("HitLine");
        hitLineCenter = hitLine.transform.position.x;
        touching = false;
        scored = false;
        noteDestroyer = hitLine.GetComponent<NoteDestroyer>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        conductor = FindObjectOfType<Conductor>();

        if (SceneManager.GetActiveScene().name == "BarLevel")
        {
            if (track == "Track1")
            {
                spriteRenderer.sprite = barUpSprite;
                destroyKeys = new KeyCode[] { KeyCode.UpArrow, KeyCode.W };
            }
            else if (track == "Track2")
            {
                spriteRenderer.sprite = barDownSprite;
                destroyKeys = new KeyCode[] { KeyCode.DownArrow, KeyCode.S };
            }
        } else if (SceneManager.GetActiveScene().name == "DuelLevel") {
            if (track == "Track1")
            {
                spriteRenderer.sprite = bulletUpSprite;
                destroyKeys = new KeyCode[] { KeyCode.UpArrow, KeyCode.W };
            } else if (track == "Track2") {
                spriteRenderer.sprite = bulletDownSprite;
                destroyKeys = new KeyCode[] { KeyCode.DownArrow, KeyCode.S };
            }
        } else if (SceneManager.GetActiveScene().name == "CherrySpittingLevel") {
            if (track == "Track1")
            {
                spriteRenderer.sprite = bucketUpSprite;
                destroyKeys = new KeyCode[] { KeyCode.UpArrow, KeyCode.W };
            } else if (track == "Track2") {
                spriteRenderer.sprite = bucketDownSprite;
                destroyKeys = new KeyCode[] { KeyCode.DownArrow, KeyCode.S };
            }
        }
    }

    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        if (CheckDestroyKeyPressed() && touching)
        {
            float currentNotePosFromCenter = Mathf.Abs(transform.position.x - hitLineCenter);

            if (currentNotePosFromCenter <= noteDestroyer.getPerfectTolerance())
            {
                GameManager.Instance.PerfectHit();
            }
            else if (currentNotePosFromCenter > noteDestroyer.getPerfectTolerance() && currentNotePosFromCenter <= noteDestroyer.getGreatTolerance())
            {
                GameManager.Instance.GreatHit();
            }
            else
            {
                GameManager.Instance.GoodHit();
            }

            conductor.DecrementActiveNotes();
            if (isLastNote)
            {
                conductor.MarkBeatmapComplete();
            }
            scored = true;
            Destroy(gameObject);
        }
    }

    private bool CheckDestroyKeyPressed()
    {
        foreach (KeyCode key in destroyKeys)
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }
        return false;
    }


    private void OnBecameInvisible()
    {
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
