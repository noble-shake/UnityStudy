using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    SpriteRenderer sr;
    float sprSize;

    // �ڱ� �ڽ��� �����Ѵ�.
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sprSize = sr.sprite.rect.width;
    }

    private void Start()
    {

    }

    /// <summary>
    /// 
    /// 
    /// </summary>
    public void EndOfAnimations() { // �ִϸ����Ϳ��� �� �Լ��� ����ϸ� ������Ʈ�� �������ȴٰ� ���� �� ��Ȳ 
        Destroy(gameObject);
    }

    public void SetSize(float _size) {

        // sr = GetComponent<SpriteRenderer>();
        // sprSize = sr.sprite.rect.width;
        Vector3 vecScale = transform.localScale;
        vecScale *= (_size / sprSize * 1.5f);
        transform.localScale = vecScale;   
    }
}
