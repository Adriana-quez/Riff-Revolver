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
    public GameObject accuracyText;
    private AccuracyText accuracy;
    void Start()
    {
        hitLine = GameObject.FindWithTag("HitLine");
        hitLineCenter = hitLine.transform.position.x;
        touching = false;
        scored = false;
        noteDestroyer = hitLine.GetComponent<NoteDestroyer>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        accuracy = accuracyText.GetComponent<AccuracyText>();

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
                accuracy.ChangeAccuracyText("perfect");
                GameManager.Instance.PerfectHit();
            }
            else if (currentNotePosFromCenter > noteDestroyer.getPerfectTolerance() && currentNotePosFromCenter <= noteDestroyer.getGreatTolerance())
            {
                accuracy.ChangeAccuracyText("great");
                GameManager.Instance.GreatHit();
            }
            else
            {
                accuracy.ChangeAccuracyText("good");
                GameManager.Instance.GoodHit();
            }

            // StartCoroutine(showAccuracy());

            scored = true;
            Destroy(gameObject);
        }
    }

    //IEnumerator showAccuracy()
    //{
    //    Debug.Log("Showing accuracy...");
    //    Vector2 accuracyTextSpawnPosition = new Vector2(transform.position.x, transform.position.y + 1f);
    //    Debug.Log("got spawn point...");
    //    GameObject currentAccuracyText = Instantiate(accuracyText, accuracyTextSpawnPosition, Quaternion.identity);
    //    Debug.Log("spawned...");
    //    yield return new WaitForSeconds(1f);
    //    Debug.Log("waited 1 second...");
    //    Destroy(currentAccuracyText);
    //    Debug.Log("Not showing accuracy anymore...");
    //}

    private void OnBecameInvisible()
    {
        if (!scored)
        {
            GameManager.Instance.NoteMissed();
        }
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
                accuracy.ChangeAccuracyText("miss");
                // showAccuracy();
                GameManager.Instance.NoteMissed();
            }
        }
    }
}
