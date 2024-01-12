using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study240112 : MonoBehaviour
{
    int global_int = 10;
    // Start is called before the first frame update
    void Start()
    {
        int iBox = 0;
        float fBox = 0f;
        string sBox = "null";
        bool bBox = false;  // default is false.

        int a = 7;
        int b = a;
        int c = b++;
        int d = a * ++b;
        Debug.Log(d);

     }

    private void def_test()
    {
        global_int++;
    }
}
