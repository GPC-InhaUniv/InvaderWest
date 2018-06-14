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
        size = zoomRate / Mathf.Sqrt(distance); // ※※루트 안 쓰고 거리 편차를 줄일 방법은 없나?※※
        transform.localScale = new Vector3(size, size, size);
    }
}
