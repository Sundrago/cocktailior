using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_selector : MonoBehaviour
{
    public GameObject[] btn = new GameObject[5];
    float targetY;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTarget(int target)
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, btn[target].transform.position.y, 0);
    }
}
