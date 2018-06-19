using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    [SerializeField]
    float speed;

    [SerializeField]
    bool isPlayer; // 플레이어가 쏜 미사일인지 여부

    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.up * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if(isPlayer)
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss") ReturnToPool();
        else
            if (other.gameObject.tag == "Player") ReturnToPool();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BackGround")
            ReturnToPool();
    }

    // 오브젝트가 화면 밖으로 빠져나가면 false로 변경
    void ReturnToPool()
    {
        this.gameObject.SetActive(false);
    }
}
