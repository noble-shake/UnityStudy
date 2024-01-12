using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study240112 : MonoBehaviour
{
    /*
     * 2024-01-12 unity class
     * [LEARNED] local variable and global variable description.
     * [LEARNED] previous and after value change operator c++ /  ++c ...
     * [LEARNED] if ~ else if, condtion.  >, <, >=, <=, ==, !
     */

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
        a--;
        Debug.Log(a);

        def_test();
        condition_test(true);
        condition_test(false);
    }

    private void def_test()
    {
        global_int++;
    }

    private void condition_test(bool condtion) {
        if (condtion)
        {
            Debug.Log($"condition is {condtion}");
        }
        else if (!condtion)
        {
            Debug.Log($"condition is {condtion}");
        }
    }
}
