using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Study240125 : MonoBehaviour
{
    /*
     * 2024-01-25 unity class
     * [PREVIOUS] Remind Arrays and multi-dimensional Arrays
     * [PREVIOUS] Remind Params. (ref, out, in, params)
     * [PROBLEM 1] 
     * [LEARNED] string different with other type. (implemented as class)
     * [LEARNED] List Type
     */

    private void Start()
    {
        int a;
        float b;
        string ccc;
    }

    // for Logging.
    private void showDebugArray<T>(T[] _value)
    {
        string value = "";
        int count = _value.Length;
        for (int iNum = 0; iNum < count; ++iNum)
        {
            value += _value[iNum];
            if (iNum != count - 1)
            {
                value += ", ";
            }
        }

        Debug.Log(value);
    }
}