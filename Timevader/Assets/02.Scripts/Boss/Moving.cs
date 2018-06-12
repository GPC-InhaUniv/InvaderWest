using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {

    private float moveVlaue;
    private Vector3 LeftBossBoundary;
    private Vector3 RightBossBoundary;

    void BossMove()
    {
        if (transform.localPosition.x < -3.0f)
        {
            moveVlaue = 1.0f;
        }
        else if (transform.localPosition.x > 3.0f)
        {
            moveVlaue = -1.0f;
        }
        transform.Translate(Vector3.left * Time.deltaTime * moveVlaue);
    }

    void Start()
    {

    }

    void Update()
    {
        BossMove();
    }
}
