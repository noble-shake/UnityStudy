using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    SpriteRenderer sr;
    float sprSize;

    // 자기 자신을 정의한다.
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
    public void EndOfAnimations() { // 애니메이터에게 이 함수를 사용하면 오브젝트가 ㅅ각제된다고 전달 된 상황 
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
