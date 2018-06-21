using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRotate : MonoBehaviour {

    void Start()
    {
        StartCoroutine(RotateCloud());
    }
    IEnumerator RotateCloud()
    {
        transform.Rotate((new Vector3(1,0) * 5f*Time.deltaTime));
        yield return null;
        StartCoroutine(RotateCloud());
    }
}
