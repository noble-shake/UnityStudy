using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("�������")]
    [SerializeField] bool isSpawn = false;
    [SerializeField] List<GameObject> listEnemy;//3?

    [SerializeField, Range(0.1f, 2.0f)] float spawnTime = 1.0f;
    float sTimer = 0.0f;//����Ÿ�̸� 
    Transform trsSpawnPoint;

    [Header("������� ī�޶󸮹�")]
    [SerializeField] Vector2 vecCamMinMax;//��ȹ�ڰ� �����ϴ� ��ġ��, ī�޶�κ���
    Vector2 vecSpawnLimit;//���� ������ ���� ���� ���� ��ġ��, ����������

    Camera mainCam;

    float gameTimer;
    bool isPatten1 = false;

    [Header("Item Drop Rate")]
    [SerializeField, Range(0.0f, 100.0f)] float itemDropRate = 0.0f;
    [SerializeField] List<GameObject> itemList = new List<GameObject>();

    private void Awake()
    {
        // SingleTon ~ GameManger is Only One Object
        if (Instance == null)
        {
            Instance = this;
        }
        else { 
            Destroy(this);
        }
    }

    private void Start()
    {
        mainCam = Camera.main;
        trsSpawnPoint = transform.Find("SpawnPoint");

        vecSpawnLimit.x = mainCam.ViewportToWorldPoint(
            new Vector3(vecCamMinMax.x, 0f)
            ).x;

        vecSpawnLimit.y = mainCam.ViewportToWorldPoint(
            new Vector3(vecCamMinMax.y, 0f)
            ).x;
    }

    void Update()
    {
        gameTimer += Time.deltaTime;
        checkSpawn();
    }

    private void checkSpawn()//���⸦ ��ȯ�ص� �Ǵ��� üũ
    {
        if (isSpawn == false) return;

        sTimer += Time.deltaTime;
        if (sTimer >= spawnTime)
        {
            sTimer = 0.0f;
            spawnEnemy();//�������
        }
    }

    private void spawnEnemy()//���⸦ �����մϴ�.
    {
        float rand = Random.Range(0.0f, 100.0f);
        GameObject objEnemy = listEnemy[0];
        if (rand < 50.0f)
        {
            objEnemy = listEnemy[0];
        }
        else if (rand < 75.0f)
        {
            objEnemy = listEnemy[1];
        }
        else
        {
            objEnemy = listEnemy[2];
        }

        Vector3 newPos = trsSpawnPoint.position;
        newPos.x = Random.Range(vecSpawnLimit.x, vecSpawnLimit.y);
        GameObject go = Instantiate(objEnemy, newPos, Quaternion.identity);

        float rate = Random.Range(0.0f, 100.0f);
        if (rate <= itemDropRate) {
            Enemy gosc = go.GetComponent<Enemy>();
            gosc.setHaveItem();
        }
    }

    public void dropItem(Vector3 _pos) {
        int raniNum = Random.Range(0, itemList.Count);
        Instantiate(itemList[raniNum], _pos, Quaternion.identity);
    }
}
