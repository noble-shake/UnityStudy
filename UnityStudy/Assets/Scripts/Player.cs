using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] Camera cam;
    [Header("화면 리밋 비율")]
    [SerializeField][Tooltip("최소비율")] Vector2 minScreen;//최소비율
    [SerializeField, Tooltip("최대비율")] Vector2 maxScreen;//최대비율

    Animator anim;
    Vector2 moveDir;
    [Header("발사위치")]
    [SerializeField] Transform trsShootPoint; 

    [Header("프리팹들")]
    [SerializeField] GameObject fabMissile;

    [Header("세팅")]
    [SerializeField] float MissileSpeed = 7f;
    [SerializeField] float MissileDamage = 1f;

    [Header("게임스타일 세팅")]
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
    /// 플레이어의 이동기능
    /// </summary>
    private void moving()
    {
        moveDir.y = Input.GetAxisRaw("Vertical");
        moveDir.x = Input.GetAxisRaw("Horizontal");//-1 0 1
        transform.position = transform.position +
            new Vector3(moveDir.x, moveDir.y, 0) * Time.deltaTime * Speed;
    }

    /// <summary>
    /// 플레이어의 이동을 제한
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
    /// 플레이어 케릭터의 애니메이션 처리
    /// </summary>
    private void animating()
    {
        anim.SetInteger("Horizontal", (int)moveDir.x);
    }

    /// <summary>
    /// 플레이어가 미사일을 발사 
    /// </summary>
    private void checkShootMissile()
    {
        if (userShoot == true && Input.GetKeyDown(KeyCode.Space))
        {
            createMissile(trsShootPoint.position, Vector3.zero);
        }
        else if (userShoot == false)//자동발사 게임이라면
        {
            timer += Time.deltaTime;
            if (timer >= shootTimer)//총알을 쏠 준비가 됨
            {
                createMissile(trsShootPoint.position, Vector3.zero);
                timer = 0.0f;
            }
        }
    }

    /// <summary>
    /// 미사일을 생성
    /// </summary>
    private void createMissile(Vector3 _pos, Vector3 _rot)
    {
        GameObject objMissile = Instantiate(fabMissile, _pos, Quaternion.Euler(_rot));
        Missile missile = objMissile.GetComponent<Missile>();

        missile.SetMissile(MissileSpeed, MissileDamage);
    }
}
