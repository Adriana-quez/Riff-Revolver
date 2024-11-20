using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusicPlayer : MonoBehaviour
{
    private AudioSource bgMusic;

    void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "IntroCutscene" && scene.name != "BarLevel") {
            DontDestroyOnLoad(transform.gameObject);
            bgMusic = GetComponent<AudioSource>();
        } else {
            DestroyImmediate(transform.gameObject);
        }
    }

    public void PlayMusic() {
        if (bgMusic.isPlaying) return;
        bgMusic.Play();
    }

    public void StopMusic() {
        bgMusic.Stop();
    }

    void Update() {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "IntroCutscene" && scene.name == "BarLevel") {
            DestroyImmediate(transform.gameObject);
        }
    }
}
