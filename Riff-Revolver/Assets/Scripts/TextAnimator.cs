using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextAnimator : MonoBehaviour
{
    Text _text;
    TMP_Text _tmpProText;
    string writer;
    
    [SerializeField] TMP_Text nextTMPText;
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
        _text = GetComponent<Text>();
        _tmpProText = GetComponent<TMP_Text>();

        if (nextBut != null) {
            writer = _tmpProText.text;
            _tmpProText.text = "";

            StartCoroutine("TypeWriterTMP");
        }

        if (mayorReact != null) {
            mayorAudio = gameObject.AddComponent<AudioSource>();
            new WaitForSeconds(4f);
            mayorAudio.PlayOneShot(mayorReact);
        }
    }

    IEnumerator TypeWriterTMP() {
        _tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

        yield return new WaitForSeconds(delayBeforeStart);

        foreach (char c in writer) {
            if (_tmpProText.text.Length > 0) {
                _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
            }

            _tmpProText.text += c;
            _tmpProText.text += leadingChar;
            yield return new WaitForSeconds(timeBtwChars);
        }

        if (leadingChar != "") {
            _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
        }

        if (nextBut != null) {
            nextBut.gameObject.SetActive(true);
        }
    }

    void Update() {
        if (Input.GetMouseButton(0)) {
            timeBtwChars = 0.00001f;
        }

        if (nextTMPText != null) {
            nextBut.onClick.AddListener(NextDialogue);
        }
    }

    void NextDialogue() {
        nextTMPText.gameObject.SetActive(true);
        _tmpProText.gameObject.SetActive(false);
        nextBut.gameObject.SetActive(false);
    }
}
