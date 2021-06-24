using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoSystem : MonoBehaviour
{
    public GameObject Cover;
    public GameObject Images;
    public GameObject Back;

    // Start is called before the first frame update
    void Start()
    {
        Cover.SetActive(false);
        Images.SetActive(false);
        Back.SetActive(false);

    }

    public void InfoButton(){
        Cover.SetActive(true);
        Images.SetActive(true);
        Back.SetActive(true);
        GameController.Putblock = null;
    }

    public void BackButton(){
        Cover.SetActive(false);
        Images.SetActive(false);
        Back.SetActive(false);
    }

}
