using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CreateStage : MonoBehaviour
{
    public static float height;
    public static int stage;
    public static int emp;
    public int CLEAR;
    public GameObject field;
    public GameObject field2;
    public GameObject cube;
    public GameObject RetryButton;
    public GameObject EndButton;
    public GameObject OkButton;
    public GameObject Cover;
    public Material color;
    //public Transform camera;
    public float startTime;
    public Text stair;
    public Text message;
    public Text submessage;
    public Text errormessage;
    public Text clear;
    public bool result;
    public static bool END;
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioSource audioSource;

    void Start(){
        height = 0f;
        stage = 1;
        emp = 0;
        END = false;
        stair.text = stage + "F";
        clear.text = "GOAL : " + CLEAR + "F";
        RetryButton.SetActive(false);
        EndButton.SetActive(false);
        OkButton.SetActive(false);
        Cover.SetActive(false);
    }

    void LateUpdate()
    {
        emp = 0;

        result = GameController.STILLPUT.All( value => value == 1 );

        if(result&&!END){
            for(int k=0; k<GameController.initp; k++){
                GameController.STILLPUT[k] = GameController.EMPTY;
            }
            height += 2.0f;
            stage += 1;

            for(int i = 0; i < GameController.N; i++){//置けなかったマスを破壊
                for(int j = 0; j < GameController.N; j++){
                    if(GameController.squares[i, j] == GameController.EMPTY)
                    {
                        emp += 1;
                        GameController.squares[i, j] = GameController.DESTROY;
                    }

                    int s = stage-1;
                    cube = GameObject.Find("s" + s + "_" + j + "-" + i);
                    if(cube != null){
                        cube.gameObject.GetComponent<Renderer>().material = color; 
                    }
                }
            }

            if(emp == 0){//全て敷き詰められた場合
                for(int i = 0; i < GameController.N; i++){
                    for(int j = 0; j < GameController.N; j++){   
                        if(GameController.squares[j, i] != GameController.DELETE){                          
                            if(((i%2==0)&&(j%2==0))||((i%2==1)&&(j%2==1)))
                            {
                                cube = Instantiate(field, new Vector3( i+0.5f, height, j+0.5f), Quaternion.identity);
                                cube.gameObject.name = "s" + stage + "_" + j + "-" + i;
                            }else{
                                cube = Instantiate(field2, new Vector3( i+0.5f, height, j+0.5f), Quaternion.identity);
                                cube.gameObject.name = "s" + stage + "_" + j + "-" + i;
                            }
                            GameController.squares[j, i] = GameController.EMPTY;
                        }
                    }
                }

                errormessage.text = "FIELD RESET";

            }else{//敷き詰められなかった場合
                for(int i = 0; i < GameController.N; i++){
                    for(int j = 0; j < GameController.N; j++){
                        if(GameController.squares[j, i] != GameController.DELETE){                               
                            if(GameController.squares[j, i] == GameController.PUT)//既においてあるブロックの上に床を生成
                            {  
                                if(((i%2==0)&&(j%2==0))||((i%2==1)&&(j%2==1))){
                                    cube = Instantiate(field, new Vector3( i+0.5f, height, j+0.5f), Quaternion.identity);
                                    cube.gameObject.name = "s" + stage + "_" + j + "-" + i;
                                }else{
                                    cube = Instantiate(field2, new Vector3( i+0.5f, height, j+0.5f), Quaternion.identity);
                                    cube.gameObject.name = "s" + stage + "_" + j + "-" + i;
                                }
                                GameController.squares[j, i] = GameController.EMPTY;
                            }
                        }
                    }
                }
            }

            emp = 0;
            stair.text = stage + "F";

            //Debug.Log("create stage");
            /*result = GameController.STILLPUT.All( value => value == 1 );
            if(result == true){
                Debug.Log("end");
                message.text = "END";
            }else{
                Debug.Log("not end");
            }*/

            for(int i=0; i<GameController.initp; i++){
                GameController.EmptyCheck(GameController.Piece[i], i);
            }

            result = GameController.STILLPUT.All( value => value == 1 );

            //Debug.Log("after create" + GameController.STILLPUT[0] + " " + GameController.STILLPUT[1] + " " + result);

            if((stage < CLEAR)&&result){
                END = true;
                message.text = "GAME OVER";
                submessage.text = "Retry?";

                audioSource = GetComponent<AudioSource>();
                audioSource.PlayOneShot(sound1);

                RetryButton.SetActive(true);
                EndButton.SetActive(true);
                Cover.SetActive(true);
                errormessage.enabled = false;
            }

            RepairPiece();//階層が上がるたびにピースの位置を補正

            if(stage == CLEAR){
                END = true;
                message.text = "CLEAR!!";

                audioSource = GetComponent<AudioSource>();
                audioSource.PlayOneShot(sound2);
                
                OkButton.SetActive(true);
                Cover.SetActive(true);
            }
        }

    }

    public void RepairPiece(){
        for(int i=0; i<GameController.initp; i++){
            float x = GameController.Piece[i].transform.position.x;
            float z = GameController.Piece[i].transform.position.z;
            GameController.Piece[i].transform.position = new Vector3(x,height+1,z);
        }
    }

}
