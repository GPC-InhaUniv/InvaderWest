using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizebyDistance : MonoBehaviour {
    [SerializeField]
    GameObject target;

    [SerializeField][Range(0f, 1f)]
    float zoomRate = 0.6f;
    float distance; // 1 ~ 105
    float size;

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);
        size = zoomRate / Mathf.Sqrt(distance);
        transform.localScale = new Vector3(size, size, size);
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().SetTrigger("Checked");
    }

    private void OnTriggerStay(Collider other)
    {
        //GetComponent<Animator>().SetTrigger("Checked");
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<Animator>().enabled = false;
        //gameObject.GetComponent<Animator>().SetTrigger("Normal");
    }

}
