using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class CameraSystem : MonoBehaviour
{

    public Transform followTfm;
    public float x;
    public float z;
 
    float smoothTime = 1.0f;
 
    Vector3 velocity = Vector3.zero;
     
    void Update () {
            // 追従対象オブジェクトのTransformから、目的地を算出
            Vector3 targetPos = new Vector3(x,16.0f+CreateStage.height,z);
            //followTfm.TransformPoint(new Vector3(0.5f, 1.0f, -1.0f));
 
            // 移動
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}*/

public class CameraSystem : MonoBehaviour
{

    public Transform followTfm;
    public GameObject Main;
    public GameObject Second;
    public float MainX;
    public float MainZ;
    public float SecondX;
    public float SecondZ;

 
    float smoothTime = 1.0f;
 
    Vector3 velocity = Vector3.zero;
     
    void Update () {
            // 追従対象オブジェクトのTransformから、目的地を算出
            Vector3 maintargetPos = new Vector3(MainX,16.0f+CreateStage.height,MainZ);
            Vector3 secondtargetPos = new Vector3(SecondX,16.0f+CreateStage.height,SecondZ);
            //followTfm.TransformPoint(new Vector3(0.5f, 1.0f, -1.0f));
 
            // 移動
            //transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
            Main.transform.position = Vector3.SmoothDamp(Main.transform.position, maintargetPos, ref velocity, smoothTime);
            Second.transform.position = Vector3.SmoothDamp(Second.transform.position, secondtargetPos, ref velocity, smoothTime);
        
    }
}
