using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditBtnControl : MonoBehaviour
{
    bool show = false;
    public GameObject credit;

    public void creditBtnClicked(){
        if(!show) {
            credit.GetComponent<Animator>().SetTrigger("show");
            show = true;
        } else {
            credit.GetComponent<Animator>().SetTrigger("hide");
            show = false;
        }
    }

    public void hidded(){
        show = false;
    }

    public void OpenInstagram(){
        if(show) Application.OpenURL("instagram://user?username=cockdail.y");
    }
}
