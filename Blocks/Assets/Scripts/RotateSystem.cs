using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*public class RotateSystem : MonoBehaviour
{
    public GameObject Parent;

    public void ParentRotate(){

        if((Parent.transform.position.x >= GameController.N) || (Parent.transform.position.z >= GameController.N)||(Parent.transform.position.x < 0)||(Parent.transform.position.z < 0)){
            Parent.transform.Rotate(new Vector3(0,90,0));
        }
    }

}*/

// IPointerClickHandlerインターフェースを継承して OnPointerClickを実装
public class RotateSystem : MonoBehaviour, IPointerClickHandler {

    
    public GameObject Parent;

    public void OnPointerClick(PointerEventData data){
        if((this.transform.position.x >= GameController.N) || (this.transform.position.z >= GameController.N)||(this.transform.position.x < 0)||(this.transform.position.z < 0)){
            this.transform.Rotate(new Vector3(0,90,0));
        }
    }

    public void ParentRotate(){

        if((Parent.transform.position.x >= GameController.N) || (Parent.transform.position.z >= GameController.N)||(Parent.transform.position.x < 0)||(Parent.transform.position.z < 0)){
            Parent.transform.Rotate(new Vector3(0,90,0));
        }
    }
}
