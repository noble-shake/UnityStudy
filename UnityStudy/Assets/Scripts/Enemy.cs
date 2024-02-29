using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] bool isBoss;

    public bool Boss 
    { 
        get { return isBoss; }
        set { isBoss = value; }
    }
    public bool BossRamda => isBoss; // set disabled.
    bool bossStartMove = false;
    private float startPosY;
    float ratioY;
    bool swayRight = false; //left right moving

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

    [SerializeField] Vector2 vecCamMinMax;//기획자가 설정하는 위치값, 카메라로부터
    Animator anim;

    private void OnBecameInvisible()
    {
        if (Boss == false) {
            Destroy(gameObject);
        }
        
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

        startPosY = transform.position.y;
    }

    void Update()
    {
        if (isBoss == false)
        {
            transform.position += -transform.up * Time.deltaTime * speed;
        }
        else {
            if (bossStartMove == false)
            {
                bossStartMoving();
            }
            else {
                bossSwayMoving();
            }
        }
        
    }

    public void Hit(float _damage)
    {
        if (Boss == true && bossStartMove == false) return;

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
            if (Boss == false)
            {
                anim.SetTrigger("Hit");
            }
            else {
                spriteRenderer.sprite = spriteHit;
                Invoke("setSpriteDefault", 0.1f);
            }

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
        if (Boss == false) {
            destroyFunction();
        }
        
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

    private void bossStartMoving() {
        ratioY += Time.deltaTime * 0.5f;
        if (ratioY >= 1.0f) {
            bossStartMove = true;
        }

        Vector3 curPos = transform.position;
        curPos.y = Mathf.SmoothStep(startPosY, 2.5f, ratioY);
        transform.position = curPos;
    }

    private void bossSwayMoving() {
        if (swayRight)
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }
        else {
            transform.position += -transform.right * Time.deltaTime * speed;
        }
        checkMoveLimit();
    }

    private void checkMoveLimit() {
        Vector3 curPos = Camera.main.WorldToViewportPoint(transform.position);
        if (swayRight == false && curPos.x < vecCamMinMax.x)
        {
            swayRight = true;
        }
        else if (swayRight == true && curPos.x > vecCamMinMax.y)
        {
            swayRight = false;
        }
    }

}
