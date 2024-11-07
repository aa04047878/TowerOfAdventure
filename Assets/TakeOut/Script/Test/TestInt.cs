using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInt : MonoBehaviour
{
    public int xd1;
    public int xd2;
    public int xd3;
    public List<int> list_xd;

    public void IntInit()
    {
        list_xd = new List<int>();

        xd1 = 0;
        xd2 = 0;
        xd3 = 0;

        list_xd.Add(xd1);
        list_xd.Add(xd2);
        list_xd.Add(xd3);

        for (int i = 0; i < list_xd.Count; i++)
        {
            list_xd[i]++;
        }
        list_xd[0] = 99999;

    }

    // Start is called before the first frame update
    void Start()
    {
        IntInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
