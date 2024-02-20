using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float hp;
    [SerializeField] float speed;

    SpriteRenderer spriteRenderer;
    Sprite spriteDefault;
    [SerializeField] Sprite spriteHit;


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteDefault = spriteRenderer.sprite;
    }

    void Update()
    {
        transform.position += -transform.up * Time.deltaTime * speed;
    }

    public void Hit(float _damage)
    {
        hp -= _damage;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            spriteRenderer.sprite = spriteHit;
            Invoke("setSpriteDefault", 0.1f);
        }
    }

    private void setSpriteDefault()
    {
        spriteRenderer.sprite = spriteDefault;
    }
}
