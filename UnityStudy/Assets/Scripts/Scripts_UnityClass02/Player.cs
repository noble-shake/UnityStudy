using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //2024-02-08
    // [LEARNED] Animation Description. Finite State Machine.
    // [LEARNED] Input Methods
    // [LEARNED] Movemant Player
    [SerializeField] float speed;
    [SerializeField] Camera cam;

    [SerializeField] Vector2 minScreen;
    [SerializeField] Vector2 maxScreen;

    Animator anim;
    Vector2 moveDir;

    void Start()
    {
       anim = GetComponent<Animator>();
    }

    void Update()
    {
        moving();   // Ctrl + '.' -> automatically Definition Generate.     
        checkMovePostion();
        animating();
    }

    private void moving()
    {
        moveDir.y = Input.GetAxisRaw("Vertical");
        moveDir.x = Input.GetAxisRaw("Horizontal"); // -1 ~ 0 ~ 1 �̹Ƿ� animation���� condition params 0 less or greather �� ���ָ� �ִϸ��̼� ����� �б� ����.
        // Debug.Log($"{vertical} / {horizontal}");
        transform.position += new Vector3(moveDir.x, moveDir.y, transform.position.z) * Time.deltaTime * speed;

        // 2024-02-13 Unity LifeCycle ~ Game Logic Section and Physics Section Collision event exist.

    }

    /// <summary>
    /// Limit Player's Moving Range.
    /// </summary>
    private void checkMovePostion() {
        // Camera View Port (cam based vector),  World Space (absolute vector)
        Vector3 curPos = cam.WorldToViewportPoint(transform.position);
        if (curPos.x < minScreen.x)
        {
            curPos.x = minScreen.x;
        }
        else if (curPos.x > maxScreen.x) {
            curPos.x = maxScreen.x;
        }

        if (curPos.y < minScreen.y)
        {
            curPos.y = minScreen.y;
        }
        else if (curPos.y > maxScreen.y)
        {
            curPos.y = maxScreen.y;
        }

        Vector3 fixedPos = cam.ViewportToWorldPoint(curPos);
        transform.position = fixedPos;
    }

    /// <summary>
    /// 
    /// List Append Condition Parameters
    /// Int, Bool, float, trigger.
    /// float, int -> compare condition
    /// bool -> 2 transition
    /// Trigger ...? 
    /// 
    /// </summary>
    private void animating() {
        // anim.SetInteger(0, (int)moveDir.x);
        anim.SetInteger("Horizontal", (int)moveDir.x);
    }

    /// <summary>
    /// Not Use Function.
    /// </summary>
    private void movingDescription() {
        // Mouse,  0 : left click, 1 right click, 2 wheel click
        Input.GetMouseButton(0); // ���콺�� �� ������ �ִ� ���� üũ
        Input.GetMouseButtonDown(0); // ���콺�� Ŭ�� ���� �� üũ
        Input.GetMouseButtonUp(0); // ���콺�� �������� üũ
        // Input.mousePosition; // mouse position

        // Keyboard
        Input.GetKey(KeyCode.A);
        Input.GetKeyDown(KeyCode.A);
        Input.GetKeyUp(KeyCode.A);

        // UnityEngine Build Setting -> Player Setting -> Input Manager -> Propertie check.
        Input.GetAxis("Horizontal");  // Left <-> Right,  -1 ~ 1 ���� ������, ���� ������ ���� ��������.
        Input.GetAxisRaw("Horizontal"); //  Left <-> Right, -1, 0, 1 ���� �ٷ� ����
        Input.GetAxis("Vertical");


    }
    
}
