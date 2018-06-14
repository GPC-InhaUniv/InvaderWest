using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {

    private int moveVlaue = 1;

    [SerializeField]
    private float moveSpeed = 3.5f;

    private float leftLimitX;
    private float rightLimitX;

    void Start()
    {
        leftLimitX = -3.0f;
        rightLimitX = 3.0f;
    }
    private void FixedUpdate()
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
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime * moveVlaue);
    }

}
