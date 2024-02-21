using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eItemType//열거형 자료형
{
    Recorvery,
    PowerUp,
}

public class Item : MonoBehaviour
{
    [SerializeField] eItemType itemType;
    private Vector3 moveDir;//움직일 방향
    private float speed;//움직이는 속도

    [SerializeField] Vector2 speedMinMax;//랜덤으로 움직일 속도 민,맥스
                                         //
    [Header("화면 리밋 비율")]
    [SerializeField][Tooltip("최소비율")] Vector2 minScreen;//최소비율
    [SerializeField, Tooltip("최대비율")] Vector2 maxScreen;//최대비율
    private Camera cam;

    void Start()
    {
        float dirX = Random.Range(-1.0f, 1.0f);
        float dirY = Random.Range(-1.0f, 1.0f);

        moveDir = new Vector2(dirX, dirY);//3에 2를 넣게되면 z는 0으로 
        //moveDir.Normalize();
        speed = Random.Range(speedMinMax.x, speedMinMax.y);

        cam = Camera.main; // singleton
    }
    
    void Update()
    {
        transform.position += moveDir * speed * Time.deltaTime;
        checkMovePosition();
    }

    private void checkMovePosition()
    {
        Vector3 curpos = cam.WorldToViewportPoint(transform.position);

        // [PROBLEM] screen 넘어가면 드득 거리는걸 해결해야 하는데. 방향 문제 일 것임.
        if (curpos.x < minScreen.x && moveDir.x < 0)
        {
            //moveDir = -moveDir;
            // normal mapping -> 항상 면의 수직 방향값. ex) CameraLight reflect
            // https://docs.unity3d.com/Manual/StandardShaderMaterialParameterNormalMap.html

            moveDir = Vector3.Reflect(moveDir, Vector3.left);
        }
        else if (curpos.x > maxScreen.x && moveDir.x > 0)
        {
            moveDir = Vector3.Reflect(moveDir, Vector3.right);
        }

        if (curpos.y < minScreen.y && moveDir.y < 0)
        {
            moveDir = Vector3.Reflect(moveDir, Vector3.up);
        }
        else if (curpos.y > maxScreen.y && moveDir.y > 0)
        {
            moveDir = Vector3.Reflect(moveDir, Vector3.down);
        }

        Vector3 fixedpos = cam.ViewportToWorldPoint(curpos);
        transform.position = fixedpos;
    }

    public eItemType GetItemType()
    {
        return itemType;
    }
}
