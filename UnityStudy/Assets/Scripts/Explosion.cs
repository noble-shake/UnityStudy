using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    SpriteRenderer sr;
    float sprSize;

    private void Awake()//�ڱ� �ڽ��� ����
    {
        sr = GetComponent<SpriteRenderer>();
        sprSize = sr.sprite.rect.width;
    }

    public void EndOfAnimation()//�ִϸ����Ϳ��� �� �Լ��� ����ϸ� ������Ʈ�� �����ȴٰ� ���� �� ��Ȳ
    {
        Destroy(gameObject);
    }

    public void SetSize(float _size)
    {
        Vector3 vecScale = transform.localScale;
        vecScale *= (_size / sprSize) * 1.5f;
        transform.localScale = vecScale;
    }
}

