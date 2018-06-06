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
        Debug.Log(other.tag);
    }

}
