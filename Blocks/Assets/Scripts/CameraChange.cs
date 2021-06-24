using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraChange : MonoBehaviour
{

    public Camera main;
    public Camera second;

    private static int TOMAIN = 0;
    private static int TOSEC = 1;
    private static int TO = TOSEC;
    public static bool change;

    // Start is called before the first frame update
    void Start()
    {
        change = false;
        main.gameObject.SetActive(true);
        second.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void ChangeToSecond()
    {
        
        change = true;

        switch(TO){
            case 0:
                GameObject.Find("MainCanvas").GetComponent<Canvas>().worldCamera = main;
                main.gameObject.SetActive(true);
                second.gameObject.SetActive(false);
                GameController.camera_object = main.GetComponent<Camera>();
                //Display.displays[0].Activate();
                TO = TOSEC;
                break;
            case 1:
                GameObject.Find("MainCanvas").GetComponent<Canvas>().worldCamera = second;
                main.gameObject.SetActive(false);
                second.gameObject.SetActive(true);
                GameController.camera_object = second.GetComponent<Camera>();
                //Display.displays[1].Activate();
                TO = TOMAIN;
                break;
        }
    }
}
