using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] Camera cam;
    [Header("ȭ�� ���� ����")]
    [SerializeField][Tooltip("�ּҺ���")] Vector2 minScreen;//�ּҺ���
    [SerializeField, Tooltip("�ִ����")] Vector2 maxScreen;//�ִ����

    Animator anim;
    Vector2 moveDir;
    [Header("�߻���ġ")]
    [SerializeField] Transform trsShootPoint; 

    [Header("�����յ�")]
    [SerializeField] GameObject fabMissile;

    [Header("����")]
    [SerializeField] float MissileSpeed = 7f;
    [SerializeField] float MissileDamage = 1f;

    [Header("���ӽ�Ÿ�� ����")]
    [SerializeField] bool userShoot;
    float timer;
    [SerializeField, Range(0.2f, 2f)] float shootTimer = 0.5f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moving();
        checkMovePosition();
        animating();

        checkShootMissile();
    }

    /// <summary>
    /// �÷��̾��� �̵����
    /// </summary>
    private void moving()
    {
        moveDir.y = Input.GetAxisRaw("Vertical");
        moveDir.x = Input.GetAxisRaw("Horizontal");//-1 0 1
        transform.position = transform.position +
            new Vector3(moveDir.x, moveDir.y, 0) * Time.deltaTime * Speed;
    }

    /// <summary>
    /// �÷��̾��� �̵��� ����
    /// </summary>
    private void checkMovePosition()
    {
        Vector3 curPos = cam.WorldToViewportPoint(transform.position);

        if (curPos.x < minScreen.x)
        {
            curPos.x = minScreen.x;
        }
        else if(curPos.x > maxScreen.x)
        {
            curPos.x = maxScreen.x;
        }

        if (curPos.y < minScreen.y)
        {
            curPos.y = minScreen.y;
        }
        else if(curPos.y > maxScreen.y)
        {
            curPos.y = maxScreen.y;
        }

        Vector3 fixedPos = cam.ViewportToWorldPoint(curPos);
        transform.position = fixedPos;
    }
    /// <summary>
    /// �÷��̾� �ɸ����� �ִϸ��̼� ó��
    /// </summary>
    private void animating()
    {
        anim.SetInteger("Horizontal", (int)moveDir.x);
    }

    /// <summary>
    /// �÷��̾ �̻����� �߻� 
    /// </summary>
    private void checkShootMissile()
    {
        if (userShoot == true && Input.GetKeyDown(KeyCode.Space))
        {
            createMissile(trsShootPoint.position, Vector3.zero);
        }
        else if (userShoot == false)//�ڵ��߻� �����̶��
        {
            timer += Time.deltaTime;
            if (timer >= shootTimer)//�Ѿ��� �� �غ� ��
            {
                createMissile(trsShootPoint.position, Vector3.zero);
                timer = 0.0f;
            }
        }
    }

    /// <summary>
    /// �̻����� ����
    /// </summary>
    private void createMissile(Vector3 _pos, Vector3 _rot)
    {
        GameObject objMissile = Instantiate(fabMissile, _pos, Quaternion.Euler(_rot));
        Missile missile = objMissile.GetComponent<Missile>();

        missile.SetMissile(MissileSpeed, MissileDamage);
    }
}
