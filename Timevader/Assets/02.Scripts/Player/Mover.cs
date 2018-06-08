using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    [SerializeField]
    private float speed;

    private void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = transform.up * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item" || other.gameObject.tag == "BackGround") return;
        Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BackGround")
            OutofScreen();
    }

    public void OutofScreen()
    {
        // 오브젝트가 화면 밖으로 빠져나가면 false로 변경
        this.gameObject.SetActive(false);
    }

}
