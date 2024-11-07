using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUIPosition : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("obj.transform.position : " + obj.transform.position);
            }
        
        
    }
}
