using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Study240118 : MonoBehaviour
{
    public Study240117 previous_study;
    /*
     * 2024-01-18 unity class
     * [PREVIOUS] remind PROBLEM 1 ~ 4 using "for". then use "while"
     * [LEARNED] Function.
     * [LEARNED] Unity Life Cycle.  https://docs.unity3d.com/Manual/ExecutionOrder.html
     * [LEARNED] Awake - Start - FixedUpdate - Update - LateUpdate 
     * [LEARNED] return - if only return or zero(0) return, exit function. or not, return pre-defining variable.
     * 
     */

    private void Awake()
    {
        // First Called. Only once run.
        // normally used define itself
        // all of Awake function order is random. setting from Project Setting "Script Execution Order"
        Debug.Log("Awake");
        
    }

    private void Start()
    {
        // Second Called. Only once run.
        // get reference from other scripts.
        Debug.Log("Start");
        previous_study = GetComponent<Study240117>();
        previous_study.problem_1_function();
        previous_study.problem_2_function();
        previous_study.problem_3_function();
        previous_study.problem_4_function();
    }

    private void FixedUpdate()
    {
        // not related frames. has specific timing.(0.02s) recommended use Physics implement
        Debug.Log("FixedUpdate");
    }
    private void Update()
    {
        // each framed run. algoritm works
        Debug.Log("Update");
    }

    private void LateUpdate()
    {
        // same with update but, need to check at alst. Camera Moving
        Debug.Log("LateUpdate");
    }

    private void function1()
    {
        
    }

    private void function2() { }
    private void function3() { }
}


