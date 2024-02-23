using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    private GameManager gameManager;
    private Image Hp;
    private Image Effect;
    private Transform trsPlayer;

    private void Awake()
    {
        Hp = transform.Find("Hp").GetComponent<Image>();
        Effect = transform.Find("Effect").GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        trsPlayer = gameManager.GetPlayerTransform();
    }

    // Update is called once per frame
    void Update()
    {
        checkPosition();
    }

    private void checkPosition() {
        if (trsPlayer == null) return;

        Vector3 movePos = trsPlayer.position;
        movePos.y -= 0.7f;
        transform.position = movePos;
    }
}
