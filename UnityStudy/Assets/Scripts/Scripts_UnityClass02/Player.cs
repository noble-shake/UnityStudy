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


    void Start()
    {
        
    }

    void Update()
    {
        moving();   // Ctrl + '.' -> automatically Definition Generate.     
    }

    private void moving()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        // Debug.Log($"{vertical} / {horizontal}");
        transform.position += new Vector3(horizontal, vertical, transform.position.z) * Time.deltaTime * speed;
    }

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
