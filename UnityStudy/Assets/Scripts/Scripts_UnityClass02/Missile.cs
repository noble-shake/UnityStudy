using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] float speed;
    float damage = 1.0f;
    bool isEnemyMissile = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3.up �� �۷ι��� ��ǥ�� �̵���Ű�Ƿ� �����̼��� ������� ����
        // transform.position = transform.position + Vector3.up * Time.deltaTime * speed;

        // transform.position = transform.position + (transform.rotation * Vector3.up)  * Time.deltaTime * speed;
        transform.position = transform.position + transform.up  * Time.deltaTime * speed; // rotation �����
        
    }

    /// <summary>
    ///  Rendering �ǰ� �ִٰ� Rendering �� �Ǿ��� ��, (ī�޶� �Ⱥ����� ��)
    ///  �ѹ��� ����.
    /// </summary>
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void SetMissile(float _speed, float _dmg) {
        speed = _speed;
        damage = _dmg;
        isEnemyMissile = true;

        // RigidBody -> Dynamic / Static equal to physics,  kinematic -> controlled by script.
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
