using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credAnimControl : MonoBehaviour
{
    public GameObject creditBtn;

    // Update is called once per frame
    void Hidden()
    {
        creditBtn.GetComponent<creditBtnControl>().hidded();
    }
}
