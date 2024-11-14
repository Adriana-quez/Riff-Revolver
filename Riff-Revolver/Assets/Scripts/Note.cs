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
    public KeyCode destroyKey;
    public bool touching;
    public bool scored;
    private SpriteRenderer spriteRenderer;
    public Sprite barSprite;
    void Start()
    {
        hitLine = GameObject.FindWithTag("HitLine");
        hitLineCenter = hitLine.transform.position.x;
        touching = false;
        scored = false;
        noteDestroyer = hitLine.GetComponent<NoteDestroyer>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (SceneManager.GetActiveScene().name == "BarLevel")
        {
            spriteRenderer.sprite = barSprite;
        }

        if (track == "Track1")
        {
            destroyKey = KeyCode.UpArrow;
        }
        else if (track == "Track2")
        {
            destroyKey = KeyCode.DownArrow;
        }
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
