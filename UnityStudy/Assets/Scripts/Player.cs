using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] Camera cam;
    [Header("ȭ�� ���� ����")]
    [SerializeField][Tooltip("�ּҺ���")] Vector2 minScreen;//�ּҺ���
    [SerializeField, Tooltip("�ִ����")] Vector2 maxScreen;//�ִ����

    Animator anim;
    Vector2 moveDir;
    [Header("�߻���ġ")]
    [SerializeField] Transform trsShootPoint; 

    [Header("�����յ�")]
    [SerializeField] GameObject fabMissile;

    [Header("����")]
    [SerializeField] float MissileSpeed = 7f;
    [SerializeField] float MissileDamage = 1f;

    [Header("���ӽ�Ÿ�� ����")]
    [SerializeField] bool userShoot;
    float timer;
    [SerializeField, Range(0.2f, 2f)] float shootTimer = 0.5f;

    [Header("�÷��̾� ����")]
    [SerializeField] int maxHp = 3;
    [SerializeField] int playerAttackLevel = 1;
    [SerializeField] int maxPlayerAttackLevel = 5;
    int hp;
    [SerializeField] GameObject fabExplosion;
    

    Sprite spriteDefault;
    SpriteRenderer spriteRenderer;

    bool isInvincibility = false;//��������
    float timerInvincibility = 0.0f;//Ÿ�̸�
    [SerializeField] float tInvincibility = 1f;//�����ð� ����
    BoxCollider2D boxCollider;

    [SerializeField] float distanceMissileX =3f;
    [SerializeField] float distanceMissileY =3f;
    [SerializeField] float angleMissile =30f;

    [Header("HP")]
    [SerializeField] PlayerHp playerHp;

    private void OnTriggerEnter2D(Collider2D collision)//collision ���� ����
    {
        if (collision.tag == Tool.GetGameTag(GameTag.Item))
        {
            Item sc = collision.GetComponent<Item>();

            eItemType type = sc.GetItemType();
            switch (type)
            {
                case eItemType.Recorvery:
                    hp++;
                    if (hp > maxHp) {
                        hp = maxHp;
                    }
                    playerHp.SetPlayerHp(hp, maxHp);
                    break;
                case eItemType.PowerUp:
                    playerAttackLevel++;
                    if (playerAttackLevel > maxPlayerAttackLevel) {
                        playerAttackLevel = maxPlayerAttackLevel;
                    }
                    break;
            }

            Destroy(collision.gameObject);
        }
        else if (collision.tag == Tool.GetGameTag(GameTag.Enemy))
        {
            if (isInvincibility == true) return;

            //�� �ı�
            Enemy sc = collision.GetComponent<Enemy>();
            sc.destroyOnBodySlam();

            Hit();
        }
    }
    void Awake()
    {
        anim = GetComponent<Animator>();
        hp = maxHp;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteDefault = spriteRenderer.sprite;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        moving();
        checkMovePosition();
        animating();

        checkShootMissile();
        checkInvincibility();
    }

    /// <summary>
    /// �÷��̾��� �̵����
    /// </summary>
    private void moving()
    {
        moveDir.y = Input.GetAxisRaw("Vertical");
        moveDir.x = Input.GetAxisRaw("Horizontal");//-1 0 1
        transform.position = transform.position +
            new Vector3(moveDir.x, moveDir.y, 0) * Time.deltaTime * Speed;
    }

    /// <summary>
    /// �÷��̾��� �̵��� ����
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
    /// �÷��̾� �ɸ����� �ִϸ��̼� ó��
    /// </summary>
    private void animating()
    {
        anim.SetInteger("Horizontal", (int)moveDir.x);
    }

    /// <summary>
    /// �÷��̾ �̻����� �߻� 
    /// </summary>
    private void checkShootMissile()
    {
        if (userShoot == true && Input.GetKeyDown(KeyCode.Space))
        {
            checkLevelAndCreateMissile();
        }
        else if (userShoot == false)//�ڵ��߻� �����̶��
        {
            timer += Time.deltaTime;
            if (timer >= shootTimer)//�Ѿ��� �� �غ� ��
            {
                checkLevelAndCreateMissile();
                timer = 0.0f;
            }
        }
    }

    private void checkLevelAndCreateMissile() {
        //trsShootPoint.position;

        switch (playerAttackLevel) {
            case 1:
                createMissile(trsShootPoint.position, Vector3.zero);
                break;
            case 2:
                createMissile(trsShootPoint.position + new Vector3(distanceMissileX, 0, 0), Vector3.zero);
                createMissile(trsShootPoint.position - new Vector3(distanceMissileX, 0, 0), Vector3.zero);
                break;
            case 3:
                createMissile(trsShootPoint.position + new Vector3(distanceMissileX, 0, 0), Vector3.zero);
                createMissile(trsShootPoint.position + new Vector3(0, distanceMissileY, 0), Vector3.zero);
                createMissile(trsShootPoint.position - new Vector3(distanceMissileX, 0, 0), Vector3.zero);
                break;
            case 4:
                createMissile(trsShootPoint.position + new Vector3(distanceMissileX, 0, 0), Vector3.zero);
                createMissile(trsShootPoint.position + new Vector3(0, distanceMissileY, 0), Vector3.zero);
                createMissile(trsShootPoint.position - new Vector3(distanceMissileX, 0, 0), Vector3.zero);
                createMissile(trsShootPoint.position + new Vector3(distanceMissileX, 0, 0), new Vector3(0, 0, -angleMissile));
                break;
            case 5:
                createMissile(trsShootPoint.position + new Vector3(distanceMissileX, 0, 0), Vector3.zero);
                createMissile(trsShootPoint.position + new Vector3(0, distanceMissileY, 0), Vector3.zero);
                createMissile(trsShootPoint.position - new Vector3(distanceMissileX, 0, 0), Vector3.zero);
                createMissile(trsShootPoint.position + new Vector3(distanceMissileX, 0, 0), new Vector3(0, 0, -angleMissile));
                createMissile(trsShootPoint.position + new Vector3(distanceMissileX, 0, 0), new Vector3(0, 0, +angleMissile));
                break;
        }

        
    }
    /// <summary>
    /// �̻����� ����
    /// </summary>
    private void createMissile(Vector3 _pos, Vector3 _rot)
    {
        GameObject objMissile = Instantiate(fabMissile, _pos, Quaternion.Euler(_rot));
        Missile missile = objMissile.GetComponent<Missile>();

        missile.SetMissile(MissileSpeed, MissileDamage);
    }

    public void Hit()
    {
        hp--;

        if (hp < 0)
        {
            hp = 0;
        }
        playerHp.SetPlayerHp(hp, maxHp);

        // Attack Debuf
        playerAttackLevel--;
        if (playerAttackLevel < 1)
        {
            playerAttackLevel = 1;
        }

        if (hp <= 0)
        {
            destroyFunction();
        }
        else
        {
            isInvincibility = true;
        }


    }

    private void checkInvincibility()
    {
        if (isInvincibility == false) return;

        if (spriteRenderer.color.a != 0.5f)//������ �Ǿ��µ� 0.5�� �ƴ϶��
        {
            setPlayerColorAlpha(0.5f);
        }

        timerInvincibility += Time.deltaTime;
        if (tInvincibility <= timerInvincibility)
        {
            isInvincibility = false;
            boxCollider.enabled = false;
            timerInvincibility = 0.0f;
            boxCollider.enabled = true;
            setPlayerColorAlpha(1f);
        }
    }

    private void setPlayerColorAlpha(float _a)
    {
        Color color = spriteRenderer.color;
        color.a = _a;
        spriteRenderer.color = color;
    }

    private void destroyFunction()
    {
        Destroy(gameObject);

        GameObject expObj = Instantiate(fabExplosion, transform.position, Quaternion.identity);
        Explosion expSc = expObj.GetComponent<Explosion>();
        expSc.SetSize(spriteDefault.rect.width);
    }
}
