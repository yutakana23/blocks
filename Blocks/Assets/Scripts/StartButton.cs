using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public AudioClip startsound;
    public AudioSource audioSource;

    public void GameStart(){
        SceneManager.LoadScene("Stage1");
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(startsound);
    }
}
