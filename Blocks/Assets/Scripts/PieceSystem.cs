using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSystem : MonoBehaviour
{
    public int size;
    public GameObject Block;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0)){//クリックが離されると
            OkCheck();//おけるか確認
            SquareCheck();//square情報更新
        }
    }

    public void OkCheck()
    {
        GameController.OKPUT = GameController.EMPTY;//EMPTYに初期化
        if(GameController.Putblock == Block){//選択しているブロックを格納
            for(int n = 0; n < transform.childCount; n++){//そのブロックの子オブジェクト数だけループ
                //位置取得
                int subx = (int)transform.GetChild(n).gameObject.transform.position.x;
                int subz = (int)transform.GetChild(n).gameObject.transform.position.z;

                if((subx < GameController.N)&&(subz < GameController.N)&&(GameController.squares[subz, subx] == GameController.PUT)){//ボードの範囲内なら
                    //if(GameController.squares[subz, subx] == GameController.PUT){//既におかれていれば
                        GameController.OKPUT = GameController.PUT;
                        Debug.Log("OK" + GameController.OKPUT);
                        break;
                    //}
                
                }
            }
            //GameController.DebugArray();
        }
    }

    public void SquareCheck()
    {
        Debug.Log(GameController.OKPUT);
        if(GameController.OKPUT == GameController.EMPTY){
            if(GameController.Putblock == Block){//選択しているブロックを格納
                for(int n = 0; n < size; n++){//そのブロックの子オブジェクト数だけループ
                    //位置取得
                    int subx = (int)transform.GetChild(n).gameObject.transform.position.x;
                    int subz = (int)transform.GetChild(n).gameObject.transform.position.z;

                    if((subx < GameController.N)&&(subz < GameController.N)){//ボードの範囲内なら
                        GameController.squares[subz, subx] = GameController.PUT;
                        Debug.Log(subz + "," + subx + "is putted");
                    }
                }
                GameController.DebugArray();
            }
        }
    }
}
