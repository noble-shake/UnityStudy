using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("�������")]
    [SerializeField] bool isSpawn = false;
    [SerializeField] List<GameObject> listEnemy;//3?
    List<GameObject> listSpawnEnemy = new List<GameObject>();

    [SerializeField, Range(0.1f, 2.0f)] float spawnTime = 1.0f;
    float sTimer = 0.0f;//����Ÿ�̸� 
    Transform trsSpawnPoint;

    [Header("������� ī�޶󸮹�")]
    [SerializeField] Vector2 vecCamMinMax;//��ȹ�ڰ� �����ϴ� ��ġ��, ī�޶�κ���
    Vector2 vecSpawnLimit;//���� ������ ���� ���� ���� ��ġ��, ����������

    Camera mainCam;

    [Header("Item Drop Rate")]
    [SerializeField, Range(0.0f, 100.0f)] float itemDropRate = 0.0f;
    [SerializeField] List<GameObject> itemList = new List<GameObject>();
    [SerializeField] GameObject objPlayer;

    [Header("gauge")]
    [SerializeField] Slider slider;
    [SerializeField] Image sliderFill;
    [SerializeField] TMP_Text textTimer;

    float bossSpawnTime = 60f;
    [SerializeField] float gameTime = 0f;
    bool spawnBoss = false;
    float colorRatio = 0f;
    Color colorBossHp;

    [SerializeField] int killCountBossSpawn = 60;
    [SerializeField] int killCount = 0;

    [Header("Score")]
    [SerializeField] TMP_Text textScore;
    int score;

    [Header("Game Over Menu")]

    [SerializeField] GameObject objGameoverMenu;
    [SerializeField] TMP_Text textGameover;
    [SerializeField] TMP_InputField iFGameover;
    [SerializeField] Button btnGameover;
    int rank = -1;
    string keyRankData = "rankData";

    public class cRank {
        public int score = 0;
        public string name = "";
    }
    List<cRank> listRanks = new List<cRank>(); // 0 ~ 9 ranks


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

        initGameoverMenu();
    }

    private void initGameoverMenu() {
        if (objGameoverMenu.activeSelf == true) {
            objGameoverMenu.SetActive(false);
        }
        btnGameover.onClick.AddListener(saveAndNextScene); // lambda or onclick function
        //btnGameover.onClick.AddListener(Test1);
        //btnGameover.onClick.AddListener(delegate { Test2(1); });
        //btnGameover.onClick.AddListener(() => Test2(1));
    }

    private void initRank()
    {
        // JSON
        // JsonConvert // In Unity, this Class will convert unity variables to TEXTT
        // jsonutilty (unity official) �� list ���ο� �ִ� ������ �Ľ� �� �� ���� ������ ����.
        // �����ս��� unity �� �� ���� ��.
        //string value = JsonConvert.SerializeObject(listRanks);
        //listRanks = JsonConvert.DeserializeObject<List<cRank>>(value);
        string rankValue = PlayerPrefs.GetString(keyRankData, string.Empty); // key, default
        if (rankValue == string.Empty)
        {
            int count = 10;
            for (int i = 0; i < count; i++)
            {
                listRanks.Add(new cRank());
            }
            rankValue = JsonConvert.SerializeObject(listRanks);
            PlayerPrefs.SetString(keyRankData, rankValue);

        }
        else {
            listRanks = JsonConvert.DeserializeObject<List<cRank>>(rankValue);
        }

    }

    private int GetRank(int _score) {
        int count = listRanks.Count;
        for (int iNum = 0; iNum < count; iNum++) { 
            cRank rank = listRanks[iNum];
            if (rank.score < _score) {
                return iNum + 1;
            }
        }

        return -1;
    }

    private void saveAndNextScene() {

        
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
        // checkKillCount(); no need to check each frame in update
    }

    private void checkKillCount()
    {
        if (spawnBoss == false && killCountBossSpawn == killCount) {
            clearEnemy();
            createBoss();
            spawnBoss = true;
            
        }
        modifySlider();
    }

    private void checkSpawn()//���⸦ ��ȯ�ص� �Ǵ��� üũ
    {
        if (isSpawn == false || spawnBoss == true) return;

        sTimer += Time.deltaTime;
        if (sTimer >= spawnTime && gameTime < bossSpawnTime - 1)
        {
            sTimer = 0.0f;

            spawnEnemy();//�������
        }
    }

    private void checkTime() {
        if (spawnBoss == true)
        {
            if (sliderFill.color != colorBossHp) {
                if (colorRatio != 1.0f) {
                    colorRatio += Time.deltaTime;
                    if (colorRatio > 1.0f) {
                        colorRatio = 1.0f;
                    }
                }
                
            }
            if (sliderFill.color != Color.red) {


                sliderFill.color = Color.red;
            }
        }
        else {
            if (sliderFill.color == Color.red) {
                sliderFill.color = new Color(0.3f, 0.7f, 1f, 1f);
            }
            gameTime += Time.deltaTime;


            if (gameTime > bossSpawnTime)
            {
                gameTime = bossSpawnTime;
                spawnBoss = true;
                isSpawn = false;
                clearEnemy();
                createBoss();
            }

            modifySlider();
        }


    }

    public void BossKill() {
        spawnBoss = false;
        isSpawn = true;
        initTimer();
    }

    public void HitBoss(int _curHp, int _maxHp) {
        textTimer.text = $"{_curHp.ToString("D2")} / {_maxHp.ToString("D4")}";
        slider.maxValue = _maxHp;
        slider.value = _curHp;
    }

    private void initTimer() {
        gameTime = 0f;
        bossSpawnTime += 60;

        if (spawnTime != 0.1f) {
            spawnTime -= 0.1f;
        }
        initSlider();
    }

    private void modifySlider()
    {
        slider.value = gameTime;
        // ToString's parameters
        // ( ) => "0010"
        // D3 ~ ���ڸ� ������ : 10 -> 010
        // N2 ..�Ҽ��� ������
        textTimer.text = $"{((int)gameTime).ToString("D2")} / {((int)slider.maxValue).ToString("D2")}";
        // textTimer.text = $"{(int)gameTime} / {slider.maxValue}";
        // System.DateTime.Now.ToString(); // os time. never use!!
        // server synchronize needs datetime

        // killcount system.
        //slider.value = killCount;
        //textTimer.text = $"{killCount} / {killCountBossSpawn}";
    }

    public void addKillCount() {

        return;

        killCount++;
        if (killCount > killCountBossSpawn) {
            killCount = killCountBossSpawn;
            spawnBoss = true;
        }

        checkKillCount();
    }

    private void initSlider() {
        slider.maxValue = bossSpawnTime;
        //slider.maxValue = killCountBossSpawn;
        slider.value = 0f;
        //textTimer.text = $"{slider.value} / {slider.maxValue}";
        textTimer.text = $"{slider.value} / {slider.maxValue}";
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
        // GameObject go = Instantiate(listEnemy[3], newPos, Quaternion.identity);
        GameObject go = listEnemy[listEnemy.Count - 1];
        Instantiate(go, trsSpawnPoint.position, Quaternion.identity);
        
    }

    private void spawnEnemy()//���⸦ �����մϴ�.
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
        // �ߺ� ó��
        //if (listSpawnEnemy.Exists((x) => x == _value) == false)
        //{
        //    listSpawnEnemy.Add(_value);
        //}
        listSpawnEnemy.Add(_value);
    }

    public void RemoveSpawnEnemyList(GameObject _value) {
        listSpawnEnemy.Remove(_value);
    }

    public void DestroyEnemy(Enemy.enumEnemy _value)
    {
        switch (_value) {
            case Enemy.enumEnemy.EnemyA:
                score += 1;
                break;
            case Enemy.enumEnemy.EnemyB:
                score += 2;
                break;
            case Enemy.enumEnemy.EnemyC:
                score += 3;
                break;
            case Enemy.enumEnemy.Boss:
                score += 4;
                break;
        }

        //textScore.text = score.ToString("D8");
        textScore.text = score.ToString("D2");

    }


    // on click button -> ���� 2�� �̻� ����� �ȵ�, ������ �ȵ�, �ڵ�� �ϴ°� ����.
    public void Test1() {
        Debug.Log("Test1");
    }

    public void Test2(int _value)
    {
        Debug.Log($"Test2 {_value}");
    }

    public void Test3(float _value)
    {
        Debug.Log("Test3");
    }

    public void Test4(bool _value)
    {
        Debug.Log("Test4");
    }
}
