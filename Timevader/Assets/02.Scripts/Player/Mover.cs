using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    [SerializeField]
    float speed;

    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.up * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Bolt") OutofScreen();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BackGround")
            OutofScreen();
    }
    // 오브젝트가 화면 밖으로 빠져나가면 false로 변경
    void OutofScreen()
    {
        this.gameObject.SetActive(false);
    }
}
