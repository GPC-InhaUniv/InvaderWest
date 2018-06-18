using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {

    int moveVlaue = 1;

    [SerializeField]
    float moveSpeed = 3.5f;

    float leftLimitX;
    float rightLimitX;

    void Start()
    {
        leftLimitX = -3.0f;
        rightLimitX = 3.0f;
    }
    void FixedUpdate()
    {
        BossMove();
    }

    void BossMove()
    {
        if (transform.localPosition.x < leftLimitX)
        {
            moveVlaue = -1;
        }
        else if (transform.localPosition.x > rightLimitX)
        {
            moveVlaue = 1;
        }
        transform.Translate(Vector3.left * moveVlaue * moveSpeed * Time.deltaTime); //지용님 수정사항 반영
    }

}
