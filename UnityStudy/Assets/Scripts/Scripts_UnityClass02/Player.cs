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
        moveDir.x = Input.GetAxisRaw("Horizontal"); // -1 ~ 0 ~ 1 이므로 animation에서 condition params 0 less or greather 를 해주면 애니메이션 컨디션 분기 가능.
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
        Input.GetMouseButton(0); // 마우스를 꾹 누르고 있는 것을 체크
        Input.GetMouseButtonDown(0); // 마우스를 클릭 했을 때 체크
        Input.GetMouseButtonUp(0); // 마우스를 떼었는지 체크
        // Input.mousePosition; // mouse position

        // Keyboard
        Input.GetKey(KeyCode.A);
        Input.GetKeyDown(KeyCode.A);
        Input.GetKeyUp(KeyCode.A);

        // UnityEngine Build Setting -> Player Setting -> Input Manager -> Propertie check.
        Input.GetAxis("Horizontal");  // Left <-> Right,  -1 ~ 1 사이 값으로, 누른 강도가 점점 쌔지도록.
        Input.GetAxisRaw("Horizontal"); //  Left <-> Right, -1, 0, 1 값을 바로 받음
        Input.GetAxis("Vertical");


    }
    
}
