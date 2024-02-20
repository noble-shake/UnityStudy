using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    //���̴��� ������ offset�� �������ִ� ����

    private Material matBottom;
    private Material matMiddle;
    private Material matTop;

    [SerializeField] private float speedBottom;
    [SerializeField] private float speedMiddle;
    [SerializeField] private float speedTop;

    void Start()
    {
        init();
    }

    private void init()
    {
        SpriteRenderer SRBottom = transform.Find("SprBottom").GetComponent<SpriteRenderer>();
        matBottom = SRBottom.material;

        SpriteRenderer SRMiddle = transform.Find("SprMiddle").GetComponent<SpriteRenderer>();
        matMiddle = SRMiddle.material;

        SpriteRenderer SRTop = transform.Find("SprTop").GetComponent<SpriteRenderer>();
        matTop = SRTop.material;
    }

    void Update()
    {
        Vector2 vecBottom = matBottom.mainTextureOffset;
        Vector2 vecMiddle = matMiddle.mainTextureOffset;
        Vector2 vecTop = matTop.mainTextureOffset;

        vecBottom += new Vector2(0, speedBottom * Time.deltaTime);
        vecMiddle += new Vector2(0, speedMiddle * Time.deltaTime);
        vecTop += new Vector2(0, speedTop * Time.deltaTime);

        vecBottom.y = Mathf.Repeat(vecBottom.y, 1.0f);
        vecMiddle.y = Mathf.Repeat(vecMiddle.y, 1.0f);
        vecTop.y = Mathf.Repeat(vecTop.y, 1.0f);

        matBottom.mainTextureOffset = vecBottom;
        matMiddle.mainTextureOffset = vecMiddle;
        matTop.mainTextureOffset = vecTop;
    }
}
