using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipingObject : MonoBehaviour {

    [SerializeField]
    GameObject arrow;

    float speed = 0;
    Vector3 startPos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector3 endPos = Input.mousePosition;
            float swipeLength = (endPos.x - this.startPos.x);

            this.speed = swipeLength / 500.0f;
        }

        transform.Translate(0, this.speed, 0);
        this.speed *= 0.98f;
    }
}
