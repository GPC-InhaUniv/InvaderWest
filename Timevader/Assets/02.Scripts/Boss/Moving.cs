using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {

    private int moveVlaue = 1;

    [SerializeField]
    private float moveSpeed = 3.5f;

    void BossMove()
    {
        if (transform.localPosition.x < -3.0f)
        {
            moveVlaue = -1;
        }
        else if (transform.localPosition.x > 3.0f)
        {
            moveVlaue = 1;
        }
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime * moveVlaue);
    }

    void Start()
    {

    }
    private void FixedUpdate()
    {
        BossMove();
    }

    void Update()
    {

    }
}
