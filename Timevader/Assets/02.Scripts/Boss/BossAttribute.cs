using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttribute : MonoBehaviour {


    public float BossHp;
    public float MaxHp;


    [SerializeField]
    private int decreaseHp;

    [SerializeField]
    private int ScoreValue;

    [SerializeField]
    private bool dead;

    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private GameObject playerexplosion;


    private void Start()
    {
        BossHp = 30000;
        MaxHp = 30000;
        decreaseHp = 15;
        ScoreValue = 10;
        dead = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary") //해당 스크립트를 적용한 것이 바운더리에 충돌한다면
        {
            return;
        }
        if(other.tag == "Enemy")
        {
            return;
        }
        if(other.tag == "Bolt")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            BossHp -= 10;

            if(BossHp == 0)
            {             
                Instantiate(playerexplosion, transform.position, transform.rotation);
                Destroy(gameObject);

                dead = true;
            }             
        }
        
        if (other.tag == "Player") //플레이어가 부딪힌다면
        {
            Instantiate(playerexplosion, other.transform.position, other.transform.rotation);
        }
    }
}
