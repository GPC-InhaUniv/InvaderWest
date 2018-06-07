using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {

    [SerializeField]
    [Range(0, 5)]
    private float movement;

    private float moveVlaue;

    void BossMove()
    {
        if (transform.localPosition.x > -0.2f)
        {
            moveVlaue = -1;
        }
        else if (transform.localPosition.x < 0.2f)
        {
            moveVlaue = 1;
        }
        transform.Translate(Vector3.up * 1.0f * Time.deltaTime * moveVlaue);
    }

    void Start()
    {
        movement = 1;
    }

    void Update()
    {
        BossMove();
    }
}
