using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRotate : MonoBehaviour {
    [SerializeField]
    float rotateValue;

    void Start()
    {
        StartCoroutine(RotateCloud());
    }
    IEnumerator RotateCloud()
    {

        transform.Rotate((new Vector3(1,0) * rotateValue * Time.deltaTime));
        yield return null;
        StartCoroutine(RotateCloud());
    }
}
