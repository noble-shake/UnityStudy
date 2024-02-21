using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] float speed;
    float damage = 1.0f;
    bool isEnemyMissile = true;
    bool isHit = false;

    //�ݶ��̴��� ������ٵ� ������ ������ �ٸ� �����ݶ��̴��� ��Ҵ��� üũ�ϰ� ������
    private void OnTriggerEnter2D(Collider2D collision)//collision�� �� �ݶ��̴��� ���� ����
    {
        // �ߺ��Ǿ� �ִ� ���⸦ �ϳ��� ó���ϰ� �ٷ� ȿ���� ����.
        if (isHit == true) return;
        
        if (collision.tag == Tool.GetGameTag(GameTag.Enemy))
        {
            isHit = true;

            Enemy enemySc = collision.GetComponent<Enemy>();
            enemySc.Hit(damage);

            Destroy(gameObject);
        }
    }

    //������ �ǰ��ִٰ� ���̻� ������ ���� �ʰ� �Ǿ�����
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        //transform.position = transform.position + Vector3.up * Time.deltaTime * speed; 
        transform.position = transform.position + transform.up * Time.deltaTime * speed;
    }

    public void SetMissile(float _speed, float _damege)
    {
        speed = _speed;
        damage = _damege;
        isEnemyMissile = true;
    }
}
