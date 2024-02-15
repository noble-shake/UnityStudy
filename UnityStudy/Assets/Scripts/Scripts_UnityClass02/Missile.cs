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
        // Vector3.up 은 글로벌한 좌표로 이동시키므로 로테이션은 고려되지 않음
        // transform.position = transform.position + Vector3.up * Time.deltaTime * speed;

        // transform.position = transform.position + (transform.rotation * Vector3.up)  * Time.deltaTime * speed;
        transform.position = transform.position + transform.up  * Time.deltaTime * speed; // rotation 고려됨
        
    }

    /// <summary>
    ///  Rendering 되고 있다가 Rendering 안 되었을 때, (카메라에 안보였을 때)
    ///  한번만 실행.
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
