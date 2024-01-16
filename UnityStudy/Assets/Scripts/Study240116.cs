using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Study240116 : MonoBehaviour
{
    /*
     * 2024-01-16 unity class
     * [PREVIOUS] remind for iterator.
     * [PROBLEM 1] 9 to 0 debugging using for.
     * [PROBLEM 2] 1 to 10 even and odd using for.
     */

    private void Start()
    {
        // [PROBLEM 1]
        for (int iNum = 9; iNum >= 0; iNum--) {
            Debug.Log(iNum);
        }

        // [PROBLEM 2]
        for (int iNum = 1; iNum <= 10; iNum++) { 
             if (iNum % 2 == 0 )
            {
                Debug.Log($"[{iNum}] even");
            }
            else if (iNum % 2 == 1)
            {
                Debug.Log($"[{iNum}] odd");
            }
        }
    }

}