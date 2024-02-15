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
    [Header("Parameters")]
    [SerializeField] float speed;
    [SerializeField] Camera cam;
    [Space]
    [Header("Border Limit")]
    [SerializeField][Tooltip("Min Border")] Vector2 minScreen;
    [SerializeField] [Tooltip("Max Border")] Vector2 maxScreen;
    [Space]
    [Header("External Prefabs")]
    [SerializeField] GameObject fabMissile;

    Animator anim;
    Vector2 moveDir;

    [Space]
    [SerializeField] Transform trsShootPoint;

    [SerializeField] float MissileSpeed = 7f;
    [SerializeField] float MissileDamage = 1f;

    [Header("game style setting")]
    [SerializeField]bool userShoot = true;
    float timer =0f;
    [SerializeField, Range(0.2f, 2.0f)] float shootTimer = 0.5f;

    void Start()
    {
       anim = GetComponent<Animator>();
    }

    void Update()
    {
        moving();   // Ctrl + '.' -> automatically Definition Generate.     
        checkMovePostion();
        animating();
        checkShootMissile();
    }


    /// <summary>
    /// Detect Editor Inspector Change.
    /// </summary>
    // private void OnValidate()
    // {
    //    Debug.Log("inspector changed");
    //}


    /// <summary>
    /// Player Shoot Missile Script
    /// </summary>
    private void checkShootMissile()
    {

        if (userShoot == true && Input.GetKeyDown(KeyCode.Space)) {
            createMissile(trsShootPoint.position, Vector3.zero);
            
        }
        else if (userShoot == false) {
            timer += Time.deltaTime;
            if (timer >= shootTimer) {
                createMissile(trsShootPoint.position, Vector3.zero);
                timer = 0f;
            }
        }
    }

    private void createMissile(Vector3 _pos, Vector3 _rot) {
        // Resources folder support in Unity Engine from naming Resources.
        // above methods not recommended..
        // GameObject goMissile = Resources.Load<GameObject>("File/FabMissile");
        // Instantiate(fabMissile, _pos, Quaternion.Euler(_rot));
        //Global Position, Local Position ~ Child always follow distance from parent
        GameObject objMissile = Instantiate(fabMissile, _pos, Quaternion.Euler(_rot));
        Missile missile = objMissile.GetComponent<Missile>();
        missile.SetMissile(MissileSpeed, MissileDamage);

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
        // anim.SetInteger("Vertical", (int)moveDir.y);
    }

    /// <summary>
    /// Trigger and Collision divided from isTrigger in inspector.
    /// IF GameObject has Collider and Rigidbody, and collide other object
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
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
