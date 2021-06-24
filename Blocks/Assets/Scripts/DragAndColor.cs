using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndColor : MonoBehaviour , IDropHandler
{
    public static int x;
    public static int z;
    public static int[] sub = new int[2];
    public static GameObject block;
    public static GameObject target;
    public Material init;
    public Material init2;
    public Material change;


    public void OnDrag()
    {

        // カメラはメインカメラを使う
        var camera = Camera.main;
        // クリック位置を取得
        var touchPosition = Input.mousePosition;
        // XY平面を作る
        var plane = new Plane(Vector3.up, CreateStage.height*(-1) /*+ (-5)*/);
        // カメラからのRayを作成
        var ray = camera.ScreenPointToRay(touchPosition);

            // rayと平面の交点を求める（交差しない可能性もある）
        if (plane.Raycast(ray, out float enter))
        {
            if((this.gameObject == GameController.Piece[0])||(this.gameObject == GameController.Piece[1])||(this.gameObject == GameController.Piece[2])){
                //Debug.Log(this.gameObject+" "+GameController.Piece[0]+" "+GameController.Piece[1]);
                block = this.gameObject;
                GameController.Putblock = block;

                //Debug.Log(block);
                block.transform.position = ray.GetPoint(enter);
                float y =  block.transform.position.y;
                y += 4;
                block.transform.position = new Vector3(block.transform.position.x,y,block.transform.position.z);
                x = (int)System.Math.Floor(block.transform.position.x);
                z = (int)System.Math.Floor(block.transform.position.z);
            }
        }
        else
        {
            // rayと平面が交差しなかったので座標が取得できなかった
            transform.position = Vector3.zero;
        }

        FieldChange();
        ChildChange();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Ondrop");
        ResetField();
    }

    public void FieldChange(){

        for(int i=0; i<GameController.N; i++){
            for(int j=0; j<GameController.N; j++){
                //if(GameController.squares[j,i] == GameController.EMPTY){
                    target = GameObject.Find("s" + CreateStage.stage + "_" + j + "-" + i);

                    if((x<GameController.N)&&(z<GameController.N)&&(x>=0)&&(z>=0)&&(GameController.Putblock != null)&&(target != null)){
                        if((j==z)&&(i==x)){
                            target.gameObject.GetComponent<Renderer>().material = change; 
                        }else if(((i%2==0)&&(j%2==0))||((i%2==1)&&(j%2==1))){
                            target.gameObject.GetComponent<Renderer>().material = init;
                        }else if(((i%2==0)&&(j%2==1))||((i%2==1)&&(j%2==0))){
                            target.gameObject.GetComponent<Renderer>().material = init2;
                        }
                    }
                //}
            }
        }
    }

    public void ChildChange(){

        if((x<GameController.N)&&(z<GameController.N)&&(x>=0)&&(z>=0)&&(block != null)){

            GameObject[] child = new GameObject[block.transform.childCount];

            for(int i=0; i<block.transform.childCount; i++){
                int[] s = ChildPos(i);
                if((sub[0]<GameController.N)&&(sub[1]<GameController.N)&&(sub[0]>=0)&&(sub[1]>=0)){
                //Debug.Log("sub" + s[1]+" "+s[0]);
                child[i] = GameObject.Find("s" + CreateStage.stage + "_" + s[1] + "-" + s[0]);
                if(child[i] != null){
                    child[i].gameObject.GetComponent<Renderer>().material = change;
                }
                //Debug.Log(child[i]);
                }
            }
        }
    }

    public static int[] ChildPos(int n){

        sub[0] = (int)System.Math.Floor(block.transform.GetChild(n).gameObject.transform.position.x);
        sub[1] = (int)System.Math.Floor(block.transform.GetChild(n).gameObject.transform.position.z);

        return sub;
    }

    public void ResetField(){
        for(int i=0; i<GameController.N; i++){
            for(int j=0; j<GameController.N; j++){
                target = GameObject.Find("s" + CreateStage.stage + "_" + j + "-" + i);

                if(target != null){
                    if(((i%2==0)&&(j%2==0))||((i%2==1)&&(j%2==1))){
                        target.gameObject.GetComponent<Renderer>().material = init;
                    }else if(((i%2==0)&&(j%2==1))||((i%2==1)&&(j%2==0))){
                        target.gameObject.GetComponent<Renderer>().material = init2;
                    }
                }
            }
        }
    }
}
