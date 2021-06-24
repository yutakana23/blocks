using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetrySystem : MonoBehaviour
{

    string sceneName;

    void Start(){
        sceneName = SceneManager.GetActiveScene ().name;
    }

    public void RetryButton(){
        SceneManager.LoadScene (sceneName);
        CreateStage.height = 0f;
        CreateStage.stage = 1;
    }
}
