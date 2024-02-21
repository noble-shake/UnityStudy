using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eItemType//������ �ڷ���
{
    Recorvery,
    PowerUp,
}

public class Item : MonoBehaviour
{
    [SerializeField] eItemType itemType;
    private Vector3 moveDir;//������ ����
    private float speed;//�����̴� �ӵ�

    [SerializeField] Vector2 speedMinMax;//�������� ������ �ӵ� ��,�ƽ�
                                         //
    [Header("ȭ�� ���� ����")]
    [SerializeField][Tooltip("�ּҺ���")] Vector2 minScreen;//�ּҺ���
    [SerializeField, Tooltip("�ִ����")] Vector2 maxScreen;//�ִ����
    private Camera cam;

    void Start()
    {
        float dirX = Random.Range(-1.0f, 1.0f);
        float dirY = Random.Range(-1.0f, 1.0f);

        moveDir = new Vector2(dirX, dirY);//3�� 2�� �ְԵǸ� z�� 0���� 
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

        // [PROBLEM] screen �Ѿ�� ��� �Ÿ��°� �ذ��ؾ� �ϴµ�. ���� ���� �� ����.
        if (curpos.x < minScreen.x && moveDir.x < 0)
        {
            //moveDir = -moveDir;
            // normal mapping -> �׻� ���� ���� ���Ⱚ. ex) CameraLight reflect
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
