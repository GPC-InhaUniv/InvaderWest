using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {

    private float moveVlaue;
    private Vector3 LeftBossBoundary;
    private Vector3 RightBossBoundary;

    void BossMove()
    {
        LeftBossBoundary = new Vector3(-2f, 0.0f, 0.0f);
        RightBossBoundary = new Vector3(2f, 0.0f, 0.0f);
        if (transform.localPosition.x > -LeftBossBoundary.x)
        {
            moveVlaue = 0.9f;
        }
        else if (transform.localPosition.x < RightBossBoundary.x)
        {
            moveVlaue = -0.9f;
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
