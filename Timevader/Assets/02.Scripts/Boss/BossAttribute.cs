using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttribute : MonoBehaviour {

    public int BossHp;
    public int MaxHp;

    [SerializeField]
    int decreaseHp, ScoreValue;

    [SerializeField]
    bool isdead;

    [SerializeField]
    GameObject explosion;

    void Start()
    {
        BossHp = 30000;
        MaxHp = 30000;
        decreaseHp = 15;
        ScoreValue = 10;

        isdead = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            return;
        }
        else if(other.tag == "Bolt")
        {
            Instantiate(explosion, transform.position, transform.rotation); //오브젝트 풀로 수정 예정
            BossHp -= 10;
            DestroyObject(other.gameObject);

            if(BossHp == 0)
            {                           
                Destroy(gameObject);

                isdead = true;
            }             
        }
    }
}
