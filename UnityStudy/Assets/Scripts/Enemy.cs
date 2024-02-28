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

    [SerializeField] GameObject fabExplosion;

    [Header("item")]
    [SerializeField] private bool hasItem = false;
    [SerializeField] private bool dropItem = false;

    private GameManager gameManager;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteDefault = spriteRenderer.sprite;

        if (hasItem == true) {
            spriteRenderer.color = new Color(0, 0.4f, 1.0f, 1.0f);
        }

        gameManager = GameManager.Instance;
        gameManager.AddSpawnEnemyList(gameObject);

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

            GameObject expObj = Instantiate(fabExplosion, transform.position, Quaternion.identity);
            Explosion expSc = expObj.GetComponent<Explosion>();
            expSc.SetSize(spriteDefault.rect.width);
            //fabExplosion

            if (hasItem == true && dropItem == false) {  // Item has but, no drop yet.
                dropItem = true;
                GameManager.Instance.dropItem(transform.position);
            }
            GameManager.Instance.addKillCount();
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

    public void setHaveItem()
    {
        hasItem = true;
    }

    public void destroyOnBodySlam() {
        destroyFunction();
    }

    private void destroyFunction() {
        Destroy(gameObject);

        GameObject expObj = Instantiate(fabExplosion, transform.position, Quaternion.identity);
        Explosion expSc = expObj.GetComponent<Explosion>();
        expSc.SetSize(spriteDefault.rect.width);
    }

    private void OnDestroy()
    {
        if (gameManager != null) {
            gameManager.RemoveSpawnEnemyList(gameObject);
        }
        
    }
}
