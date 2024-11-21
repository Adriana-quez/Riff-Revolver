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
    public Sprite barUpSprite;
    public Sprite barDownSprite;
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
            if (track == "Track1")
            {
                spriteRenderer.sprite = barUpSprite;
                destroyKey = KeyCode.UpArrow;
            }
            else if (track == "Track2")
            {
                spriteRenderer.sprite = barDownSprite;
                destroyKey = KeyCode.DownArrow;
            }
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
                GameManager.Instance.PerfectHit(transform, track);
            }
            else if (currentNotePosFromCenter > noteDestroyer.getPerfectTolerance() && currentNotePosFromCenter <= noteDestroyer.getGreatTolerance())
            {
                GameManager.Instance.GreatHit(transform, track);
            }
            else
            {
                GameManager.Instance.GoodHit(transform, track);
            }

            scored = true;
            Destroy(gameObject);
        }
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
                GameManager.Instance.NoteMissed(transform, track);
            }
        }
    }
}
