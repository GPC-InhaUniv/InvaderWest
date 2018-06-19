using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStatus : MonoBehaviour {

    public int BossHp;
    public int MaxHp;

    [SerializeField]
    int decreaseHp, ScoreValue;

    [SerializeField]
    bool isdead;

    [SerializeField]
    GameObject explosion;

    [SerializeField]
    Slider hpBar;

    [SerializeField]
    float moveSpeed = 3.5f;

    int moveVlaue = 1;

    float leftLimitX;
    float rightLimitX;

    void FixedUpdate()
    {
        BossMove();
    }

    void Update()
    {
        hpBar.value = BossHp / MaxHp;
    }

    void Start()
    {
        leftLimitX = -2.5f;
        rightLimitX = 2.5f;

        BossHp = 30000;
        MaxHp = 30000;
        decreaseHp = 15;
        ScoreValue = 10;

        isdead = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            return;
        }
        else if (other.tag == "Bolt")
        {
            Instantiate(explosion, transform.position, transform.rotation); //오브젝트 풀로 수정 예정

            BossHp -= 10;
            DestroyObject(other.gameObject);

            if (BossHp == 0)
            {
                Destroy(gameObject);

                isdead = true;
            }
        }
    }

    void BossMove()
    {
        if (transform.localPosition.x < leftLimitX)
        {
            moveVlaue = -1;
        }
        else if (transform.localPosition.x > rightLimitX)
        {
            moveVlaue = 1;
        }
        transform.Translate(Vector3.left * moveVlaue * moveSpeed * Time.deltaTime); //지용님 수정사항 반영
    }
}
