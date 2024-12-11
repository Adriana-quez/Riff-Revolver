using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextAnimator : MonoBehaviour
{
    Text dialogue;
    TMP_Text tmpProDialogue;
    string writer;
    Coroutine animateText;
    
    [SerializeField] TMP_Text nextTMPDialogue;
    public AudioClip mayorReact;
    private AudioSource mayorAudio;

    [SerializeField] float delayBeforeStart = 0f;
    [SerializeField] float timeBtwChars = 0.1f;
    [SerializeField] string leadingChar = "";
    [SerializeField] bool leadingCharBeforeDelay = false;
    [SerializeField] Button nextBut;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = GetComponent<Text>();
        tmpProDialogue = GetComponent<TMP_Text>();

        if (nextBut != null) {
            writer = tmpProDialogue.text;
            tmpProDialogue.text = "";

            animateText = StartCoroutine("TypeWriterTMP");
        }

        if (mayorReact != null) {
            mayorAudio = gameObject.AddComponent<AudioSource>();
            new WaitForSeconds(4f);
            mayorAudio.PlayOneShot(mayorReact);
        }
    }

    IEnumerator TypeWriterTMP() {
        tmpProDialogue.text = leadingCharBeforeDelay ? leadingChar : "";

        yield return new WaitForSeconds(delayBeforeStart);

        foreach (char c in writer) {
            if (tmpProDialogue.text.Length > 0) {
                tmpProDialogue.text = tmpProDialogue.text.Substring(0, tmpProDialogue.text.Length - leadingChar.Length);
            }

            tmpProDialogue.text += c;
            tmpProDialogue.text += leadingChar;
            yield return new WaitForSeconds(timeBtwChars);
        }

        if (leadingChar != "") {
            tmpProDialogue.text = tmpProDialogue.text.Substring(0, tmpProDialogue.text.Length - leadingChar.Length);
        }

        if (nextBut != null) {
            nextBut.gameObject.SetActive(true);
        }
    }

    void Update() {
        if (Input.GetMouseButton(0)) {
            StopCoroutine(animateText);
            tmpProDialogue.text = writer;
            nextBut.gameObject.SetActive(true);
        }

        if (nextTMPDialogue != null) {
            nextBut.onClick.AddListener(NextDialogue);
        }
    }

    void NextDialogue() {
        nextTMPDialogue.gameObject.SetActive(true);
        tmpProDialogue.gameObject.SetActive(false);
        nextBut.gameObject.SetActive(false);
    }
}
