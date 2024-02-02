using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    // 2024-02-02
    // Access to Shader and Control Offset for Map Scrolling.
    // Check the Sprites Wrap Mode (clamp/repeat)
    // Check Material Shader Info (Properties Name may different.)

    // you can use SerializedField, but can be implemented in script and prevent to designer's mistake.
    private Material matBottom;
    private Material matMiddle;
    private Material matTop; // CLASS => call by ref

    [SerializeField] private float speedBottom;
    [SerializeField] private float speedMiddel;
    [SerializeField] private float speedTop;

    // private SpriteRenderer sprRenderer;


    private void init()
    {
        // if you want find GameObject.  (1), (2) conditionally slow. (3) already know itself
        // (1). GameObject.Find will search at Root Object to Last Object. it is very unefficient and slow. NOT RECOMMENDED but indispensable.
        GameObject objBottom = GameObject.Find("GroundManager/SprBottom");
        // (2). use strategy for SprBottom Object have both transform and SpriteRenderer;
        Transform trsBottom = Transform.FindObjectOfType<SpriteRenderer>().transform;

        // (3). "gameObject" or "transform". this variables are default variable from Object owned this script
        // From Parent Object To Child Object
        // transform.Find("Root").transform.Find("Parent").transform.Find("Child")  ===> transform.Find("Root/Parent/Child")
        transform.Find("SprBottom");

        // (4) if you easily know. this object's childs indexes.
        // transform.GetChild(0);


    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
