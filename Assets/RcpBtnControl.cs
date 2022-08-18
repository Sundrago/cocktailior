using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RcpBtnControl : MonoBehaviour
{
    public Text textObj;
    public void ChangeText(string input){
        textObj.text = input;
        if(input == "hide") {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
    }

    public string ReturnText(){
        return textObj.text;
    }
}
