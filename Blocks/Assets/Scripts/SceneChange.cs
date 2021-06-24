using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public AudioClip buttonsound;
    public AudioSource audioSource;

    public void StageSelect(){
        if(buttonsound != null){
            Sound();
        }
        SceneManager.LoadScene("StageSelect");
    }

    public void Stage1(){
        Sound();
        SceneManager.LoadScene("Stage1");
    }

    public void Stage2(){
        Sound();
        SceneManager.LoadScene("Stage2");
    }

    public void Stage3(){
        Sound();
        SceneManager.LoadScene("Stage3");
    }

    public void Stage4(){
        Sound();
        SceneManager.LoadScene("Stage4");
    }

    public void Stage5(){
        Sound();
        SceneManager.LoadScene("Stage5");
    }

    public void Stage6(){
        Sound();
        SceneManager.LoadScene("Stage6");
    }

    public void Stage7(){
        Sound();
        SceneManager.LoadScene("Stage7");
    }

    public void Stage8(){
        Sound();
        SceneManager.LoadScene("Stage8");
    }

    public void Stage9(){
        Sound();
        SceneManager.LoadScene("Stage9");
    }

    public void EndGame(){
        SceneManager.LoadScene("Title");
    }

    public void Sound(){
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(buttonsound);
    }
}
