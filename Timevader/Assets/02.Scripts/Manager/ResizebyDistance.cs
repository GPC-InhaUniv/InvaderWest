using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizebyDistance : MonoBehaviour {
    [SerializeField]
    GameObject target;

    [SerializeField][Range(1f, 3f)]
    float zoomRate = 0.6f;
    float distance;
    float size;

    // Update is called once per frame
    void Update()
    {
        //distance = Vector3.Distance(transform.position, target.transform.position);
        //size = zoomRate / Mathf.Sqrt(distance); // ※※루트 안 쓰고 거리 편차를 줄일 방법은 없나?※※
        distance = Mathf.Abs(transform.position.y - target.transform.position.y) + 5.0f; //0.32 ~ 53
        size = zoomRate / distance;
        transform.localScale = new Vector3(size, size, size);
    }
}
