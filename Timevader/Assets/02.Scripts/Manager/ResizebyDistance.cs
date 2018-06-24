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
        distance = Mathf.Abs(transform.position.y - target.transform.position.y) + 5.0f;
        size = zoomRate / distance;
        transform.localScale = new Vector3(size, size, size);
    }
}
