using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("적기생성")]
    [SerializeField] bool isSpawn = false;
    [SerializeField] List<GameObject> listEnemy;//3?
    List<GameObject> listSpawnEnemy = new List<GameObject>();

    [SerializeField, Range(0.1f, 2.0f)] float spawnTime = 1.0f;
    float sTimer = 0.0f;//스폰타이머 
    Transform trsSpawnPoint;

    [Header("적기생성 카메라리밋")]
    [SerializeField] Vector2 vecCamMinMax;//기획자가 설정하는 위치값, 카메라로부터
    Vector2 vecSpawnLimit;//월드 포지션 기준 생성 리밋 위치값, 월드포지션

    Camera mainCam;

    bool isPatten1 = false;

    [Header("Item Drop Rate")]
    [SerializeField, Range(0.0f, 100.0f)] float itemDropRate = 0.0f;
    [SerializeField] List<GameObject> itemList = new List<GameObject>();

    [SerializeField] GameObject objPlayer;

    [Header("gauge")]
    [SerializeField] Slider slider;
    [SerializeField] Image sliderFill;

    float bossSpawnTime = 60f;
    [SerializeField] float gameTime = 0f;
    bool spawnBoss = false;
    

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
        initSlider();
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
        checkSpawn();
        checkTime();
    }

    private void checkSpawn()//적기를 소환해도 되는지 체크
    {
        if (isSpawn == false) return;

        sTimer += Time.deltaTime;
        if (sTimer >= spawnTime)
        {
            sTimer = 0.0f;
            spawnEnemy();//적기생산
        }
    }

    private void checkTime() {
        if (spawnBoss == true) return;

        gameTime += Time.deltaTime;
        

        if (gameTime > bossSpawnTime) {
            spawnBoss = true;
            isSpawn = false;
            clearEnemy();
            createBoss();
        }

        modifySlider();
    }

    private void modifySlider()
    {
        slider.value = gameTime;
    }

    private void initSlider() {
        slider.maxValue = bossSpawnTime;
        slider.value = 0f;
    }

    private void clearEnemy() {
        int count = listSpawnEnemy.Count;
        for (int iNum = count - 1; iNum > -1; --iNum) {
            Destroy(listSpawnEnemy[iNum]);
        }
        // listSpawnEnemy.Clear();
    }

    private void createBoss()
    {
        throw new NotImplementedException();
    }

    private void spawnEnemy()//적기를 생산합니다.
    {
        float rand = UnityEngine.Random.Range(0.0f, 100.0f);
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
        newPos.x = UnityEngine.Random.Range(vecSpawnLimit.x, vecSpawnLimit.y);
        GameObject go = Instantiate(objEnemy, newPos, Quaternion.identity);

        // listSpawnEnemy.Add(go);
        AddSpawnEnemyList(go);

        float rate = UnityEngine.Random.Range(0.0f, 100.0f);
        if (rate <= itemDropRate) {
            Enemy gosc = go.GetComponent<Enemy>();
            gosc.setHaveItem();
        }
    }

    public void dropItem(Vector3 _pos) {
        int raniNum = UnityEngine.Random.Range(0, itemList.Count);
        Instantiate(itemList[raniNum], _pos, Quaternion.identity);
    }

    public Transform GetPlayerTransform() {
        if (objPlayer == null)
        {
            return null;
        }
        else {
            return objPlayer.transform;
        }
    }

    public void AddSpawnEnemyList(GameObject _value) {
        // 중복 처리
        //if (listSpawnEnemy.Exists((x) => x == _value) == false)
        //{
        //    listSpawnEnemy.Add(_value);
        //}
        listSpawnEnemy.Add(_value);
    }

    public void RemoveSpawnEnemyList(GameObject _value) {
        listSpawnEnemy.Remove(_value);
    }
}
