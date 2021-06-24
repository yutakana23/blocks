using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public const int N = 8;//ボードのマス
    public int ps;//パーツ種類
    public const int initp = 3;//生成するピースの数
    public static int pnumber;
    public static int[,] squares = new int[N, N];
 
    public int LEVEL;
    public static int EMPTY = 0;
    public static int PUT = 1;
    public static int DESTROY = 2;//もう置けないマス
    public static int DELETE = 3;
    public static int OKPUT = 0;//1が置けない0が置ける
    public static int[] STILLPUT = new int[initp];//まだ置けるか
    //public static bool END = false;//ゲーム終了
 
    //カメラ情報
    public static Camera camera_object;
    private RaycastHit hit;

    //表示文字列
    public Text errormessage;

    public GameObject[] blocks;//パーツ管理
    public static GameObject Putblock;//選択しているブロック
    public GameObject OriginObject;//
    public static GameObject[] Piece;
    public GameObject Clone;
 
    public bool[] pieces;//パーツ選択

    public GameObject clickedGameObject;

    public AudioClip putsound;
    public AudioSource audioSource;

    PieceSystem Script;

    void Start()
    {
        Piece = new GameObject[]{};
        //配列を初期化
        InitializeArray();

        switch(LEVEL){
            case 1:
                Level1Array();
                break;
            case 2:
                Level2Array();
                break;
            case 3:
                Level3Array();
                break;
            case 4:
                Level4Array();
                break;
            case 5:
                Level5Array();
                break;
            case 6:
                Level6Array();
                break;
            case 7:
                Level7Array();
                break;
            case 8:
                Level8Array();
                break;
            case 9:
                Level9Array();
                break;
        }

        InitPieces();

        //カメラ情報を取得
        camera_object = GameObject.Find("Main Camera").GetComponent<Camera>();
 
    }

    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            errormessage.text = "";

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
 
            if (Physics.Raycast(ray, out hit)) {
                Putblock = hit.collider.gameObject;
            }
        }

        if (Input.GetMouseButtonUp(0))//クリック後
        {
                //座標調整
                if(Putblock != null){
                    int x = (int)System.Math.Floor(Putblock.transform.position.x);
                    int z = (int)System.Math.Floor(Putblock.transform.position.z);
                    Vector3 pos = Putblock.transform.position;
                    pos.x = x + 0.5f;
                    pos.y = 1.0f + 2.0f * (CreateStage.stage - 1);
                    pos.z = z + 0.5f;

                    //ボード内でマスが空のとき
                    if((x<N)&&(z<N)&&(x>=0)&&(z>=0)){
                        if(squares[z,x] == EMPTY)
                        {
                            OkCheck();//置けるかチェック
                            if((OKPUT == EMPTY)&&((Putblock==Piece[0])||(Putblock==Piece[1])||(Putblock==Piece[2]))){
                                squares[z, x] = PUT;//おいてるブロックの親オブジェクトをPUTに
                                SquareChange();//子オブジェクトもPUTに
                                Putblock.transform.position = pos;//ブロック配置

                                audioSource = GetComponent<AudioSource>();
                                audioSource.PlayOneShot(putsound);

                                MakePieces(Putblock);//新しいオブジェクト生成
                            }
                        }else{
                            if(CameraChange.change&&!CreateStage.END){
                                errormessage.text = "";
                            }else{
                                errormessage.text = "CAN'T PUT THERE";
                            }

                            CameraChange.change = false;
                        }
                    }
                }
        }
    }


    //配列情報を初期化する
    public void InitializeArray()
    {
        //for文を利用して配列にアクセスする
        for (int i = 0; i < N;i++)
        {
            for (int j = 0; j < N;j++)
            {
                //配列を空（値を０）にする
                squares[i, j] = EMPTY;
            }
        }
    }

    public void Level1Array(){
        for(int i=0; i<N; i++){
            squares[i,6] = DELETE;
            squares[6,i] = DELETE;
        }

        for(int j=0; j<N; j++){
            squares[j,7] = DELETE;
            squares[7,j] = DELETE;
        }
    }

    public void Level2Array(){
        squares[3,0] = DELETE;
        squares[0,3] = DELETE;
        squares[3,6] = DELETE;
        squares[6,3] = DELETE;
        //squares[4,3] = DELETE;
        //squares[2,3] = DELETE;
        squares[3,3] = DELETE;
        //squares[3,2] = DELETE;
        //squares[3,4] = DELETE;
        for(int i=0; i<N; i++){
            squares[i,7] = DELETE;
            squares[7,i] = DELETE;
        }

    }

    public void Level3Array(){
        squares[0,0] = DELETE;
        squares[0,6] = DELETE;
        squares[6,0] = DELETE;
        squares[6,6] = DELETE;
        squares[2,2] = DELETE;
        squares[2,4] = DELETE;
        squares[4,2] = DELETE;
        squares[4,4] = DELETE;

        for(int i=0; i<N; i++){
            squares[i,7] = DELETE;
            squares[7,i] = DELETE;
        }

    }

    public void Level4Array(){
        squares[0,2] = DELETE;
        squares[0,3] = DELETE;
        squares[0,4] = DELETE;
        squares[0,5] = DELETE;
        squares[1,2] = DELETE;
        squares[1,3] = DELETE;
        squares[1,4] = DELETE;
        squares[1,5] = DELETE;
        squares[4,0] = DELETE;
        squares[4,3] = DELETE;
        squares[4,4] = DELETE;
        squares[4,7] = DELETE;
        squares[5,0] = DELETE;
        squares[5,3] = DELETE;
        squares[5,4] = DELETE;
        squares[5,7] = DELETE;
        squares[6,0] = DELETE;
        squares[6,1] = DELETE;
        squares[6,6] = DELETE;
        squares[6,7] = DELETE;
        squares[7,0] = DELETE;
        squares[7,1] = DELETE;
        squares[7,2] = DELETE;
        squares[7,5] = DELETE;        
        squares[7,6] = DELETE;
        squares[7,7] = DELETE;

    }

    public void Level5Array(){
         for(int i=0; i<N; i++){
            squares[i,7] = DELETE;
            squares[7,i] = DELETE;
        }

        for(int i=0; i<N; i++){
            for(int j=0; j<N; j++){
                if((i%2==0)&&(j%2==0)){
                    squares[j,i] = DELETE;
                }
            }
        }
    }

    public void Level6Array(){
        for(int i=1; i<=6;i++){
            squares[3,i] = DELETE;
            squares[4,i] = DELETE;
        }

        for(int i=3; i<=4;i++){
            squares[1,i] = DELETE;
            squares[2,i] = DELETE;
            squares[5,i] = DELETE;
            squares[6,i] = DELETE;
        }
    }

    public void Level7Array(){
        for(int i=0; i<=7;i++){

            squares[7,i] = DELETE;

            if(i!=4){
                squares[0,i] = DELETE;
                squares[6,i] = DELETE;
            }

            if((i!=4)&&(i!=5)){
                squares[1,i] = DELETE;
                squares[5,i] = DELETE;
            }
        }

        squares[4,7] = DELETE;
        squares[2,7] = DELETE;
    }

    public void Level8Array(){
        for(int i=0; i<=7;i++){
            if((i!=2)&&(i!=3)&&(i!=4)&&(i!=5)){
                squares[7,i] = DELETE;
                squares[0,i] = DELETE;
            }

            if((i!=1)&&(i!=2)&&(i!=5)&&(i!=6)){
                squares[6,i] = DELETE;
                squares[1,i] = DELETE;
            }

            if((i!=0)&&(i!=1)&&(i!=6)&&(i!=7)){
                squares[5,i] = DELETE;
                squares[2,i] = DELETE;
            }

            if((i!=0)&&(i!=7)){
                squares[4,i] = DELETE;
                squares[3,i] = DELETE;
            }
        }
    }

    public void Level9Array(){
        for(int i=0; i<=7;i++){
            if((i!=1)&&(i!=5)){
                squares[0,i] = DELETE;
            }

            if((i==3)||(i==7)){
                squares[1,i] = DELETE;
            }

            if((i==0)||(i==2)){
                squares[2,i] = DELETE;
            }

            if((i==0)||(i==1)||(i==3)||(i==7)){
                squares[3,i] = DELETE;
            }

            if((i==0)||(i==4)||(i==6)||(i==7)){
                squares[4,i] = DELETE;
            }

            if((i==5)||(i==7)){
                squares[5,i] = DELETE;
            }

            if((i==0)||(i==4)){
                squares[6,i] = DELETE;
            }

            if((i!=2)&&(i!=6)){
                squares[7,i] = DELETE;
            }
        }
    }

    //オブジェクト初期化
    public void InitPieces()
    {
        Piece = new GameObject[initp];

        for (int i=0; i<initp; i++)
        {
            pnumber = Random.Range(0,ps);
            //Piece = blocks[pnumber];

            if(i==0) {
                Clone = Instantiate (blocks[pnumber], new Vector3 (10.0f, CreateStage.height+1, 1.0f), Quaternion.identity);      
            }else if(i==1){
                Clone = Instantiate (blocks[pnumber], new Vector3 (10.0f, CreateStage.height+1, 5.0f), Quaternion.identity);
            }else if(i==2){
                Clone = Instantiate (blocks[pnumber], new Vector3 (14.0f, CreateStage.height+1, 3.0f), Quaternion.identity);
            }

            Piece[i] = Clone;
            EmptyCheck(Clone, i);
        }
    
    }

    //オブジェクト生成
    public void MakePieces(GameObject put){

        pnumber = Random.Range(0,ps);
        int num=2;

        for(int i=0; i<initp; i++){
            if(Piece[i] == put){
                if(i==0) {
                    Clone = Instantiate (blocks[pnumber], new Vector3 (10.0f, CreateStage.height+1, 1.0f), Quaternion.identity);
                    num = i;      
                }else if(i==1){
                    Clone = Instantiate (blocks[pnumber], new Vector3 (10.0f, CreateStage.height+1, 5.0f), Quaternion.identity);
                    num = i;
                }else if(i==2){
                    Clone = Instantiate (blocks[pnumber], new Vector3 (14.0f, CreateStage.height+1, 3.0f), Quaternion.identity);
                    num = i;
                }

                Piece[i] = Clone;
            }

            EmptyCheck(Piece[i], i);
        }
    }
 
    //配列情報を確認する（デバッグ用）
    public static void DebugArray()
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                Debug.Log("(i,j) = (" + i + "," + j + ") = " + squares[i, j]);
            }
        }
    }

    //置くときにブロックを置けるかどうかチェック
    public void OkCheck()
    {
        OKPUT = EMPTY;//EMPTYに初期化
        for(int n = 0; n < Putblock.transform.childCount; n++){//そのブロックの子オブジェクト数だけループ
                
            //位置取得
            int subx = (int)System.Math.Floor(Putblock.transform.GetChild(n).gameObject.transform.position.x);
            int subz = (int)System.Math.Floor(Putblock.transform.GetChild(n).gameObject.transform.position.z);

            if((subx >= N)||(subz >= N)||(subx < 0)||(subz < 0)||(squares[subz, subx] == PUT)||(squares[subz, subx] == DESTROY)||(squares[subz, subx] == DELETE)){//ボードの範囲外または範囲内で既におかれていれば
                    OKPUT = PUT;
                    
                    //if((squares[subz, subx] == PUT)||(squares[subz, subx] == DESTROY)||(squares[subz, subx] == DELETE)){
                        //まだゲーム中でカメラ視点変更した場合は空に
                        if(CameraChange.change&&!CreateStage.END){
                            errormessage.text = "";
                        }else{
                            errormessage.text = "CAN'T PUT THERE";
                        }

                        CameraChange.change = false;
                    //}
                    break;
            }
        }
    }

    //子オブジェクトのスクエアもPUTに
    public static void SquareChange()
    {
        for(int n = 0; n < Putblock.transform.childCount; n++){//そのブロックの子オブジェクト数だけループ
                //位置取得
            int subx = (int)System.Math.Floor(Putblock.transform.GetChild(n).gameObject.transform.position.x);
            int subz = (int)System.Math.Floor(Putblock.transform.GetChild(n).gameObject.transform.position.z);

            if((subx < N)&&(subz < N)){//ボードの範囲内なら
                squares[subz, subx] = PUT;
            }
        }
    }

    //フィールド上にブロックがまだ置けるか確認
    public static void EmptyCheck(GameObject ClonePiece, int number)
    {
        STILLPUT[number] = PUT;

        /*Debug.Log("\n" + squares[7,0] + "" +squares[7,1] + "" +squares[7,2] + "" +squares[7,3] + "" +squares[7,4] + "" +squares[7,5] + "" +squares[7,6]+ "" +squares[7,7]+"\n" 
            +squares[6,0] + "" +squares[6,1] + "" +squares[6,2] + "" +squares[6,3] + "" +squares[6,4] + "" +squares[6,5] + "" +squares[6,6]+ "" +squares[6,7]+"\n"
            +squares[5,0] + "" +squares[5,1] + "" +squares[5,2] + "" +squares[5,3] + "" +squares[5,4] + "" +squares[5,5] + "" +squares[5,6]+ "" +squares[5,7]+"\n"
            +squares[4,0] + "" +squares[4,1] + "" +squares[4,2] + "" +squares[4,3] + "" +squares[4,4] + "" +squares[4,5] + "" +squares[4,6]+ "" +squares[4,7]+"\n"
            +squares[3,0] + "" +squares[3,1] + "" +squares[3,2] + "" +squares[3,3] + "" +squares[3,4] + "" +squares[3,5] + "" +squares[3,6]+ "" +squares[3,7]+"\n"
            +squares[2,0] + "" +squares[2,1] + "" +squares[2,2] + "" +squares[2,3] + "" +squares[2,4] + "" +squares[2,5] + "" +squares[2,6]+ "" +squares[2,7]+"\n"
            +squares[1,0] + "" +squares[1,1] + "" +squares[1,2] + "" +squares[1,3] + "" +squares[1,4] + "" +squares[1,5] + "" +squares[1,6]+ "" +squares[1,7]+"\n"
            +squares[0,0] + "" +squares[0,1] + "" +squares[0,2] + "" +squares[0,3] + "" +squares[0,4] + "" +squares[0,5] + "" +squares[0,6]+ "" +squares[0,7]);
        */

        for(int i=0; i<N; i++){
            for(int j=0; j<N; j++){//各[j,i]について
                if(squares[j,i] == EMPTY){//親ブロックがemptyなら

                if(Piece[number].transform.childCount == 0){ // Ablockのとき
                    STILLPUT[number] = EMPTY;
                    break;
                }

                    for(int m=0; m<4; m++){//各回転について

                        ClonePiece.transform.Rotate(new Vector3(0,90,0));


                        GameObject C = ClonePiece;
                        for(int n=0; n<Piece[number].transform.childCount; n++){//各子ブロックについて

                            int subx = i + (int)System.Math.Round(C.transform.position.x) - (int)System.Math.Round(C.transform.GetChild(n).position.x);
                            int subz = j + (int)System.Math.Round(C.transform.position.z) - (int)System.Math.Round(C.transform.GetChild(n).position.z);

                            if((subx >= N)||(subz >= N)||(subx < 0)||(subz < 0)||(squares[subz, subx] == PUT)||(squares[subz, subx] == DESTROY)||(squares[subz, subx] == DELETE)){//ボードの範囲外または範囲内で既におかれていれば
                                STILLPUT[number] = PUT;
                                break;
                            }

                            if(n==Piece[number].transform.childCount-1){
                                STILLPUT[number] = EMPTY;
                                break;
                            }

                        }
                    
                        if(STILLPUT[number] == EMPTY){
                            break;
                        }
                    }

                    ClonePiece.transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                if(STILLPUT[number] == EMPTY){
                    break;
                }
            }

            if(STILLPUT[number] == EMPTY){
                break;
            }
        }
    }
}
