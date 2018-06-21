using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightMover : MonoBehaviour {
    int moveVlaue = 1;

    [SerializeField]
    float moveSpeed;

    float leftLimitX, rightLimitX;

    [SerializeField]
    GameState nowGameState;

    void Start()
    {
        StartCoroutine("checkGameState");

        leftLimitX = -2.0f;
        rightLimitX = 2.0f;
    }

    void FixedUpdate()
    {
        if (nowGameState == GameState.Started)
        {
            LeftRightMove();
        }
    }

    public void LeftRightMove()
    {
        if (transform.localPosition.x < leftLimitX) //-2
        {
            moveVlaue = -1;
        }
        else if (transform.localPosition.x > rightLimitX) //2
        {
            moveVlaue = 1;
        }
        transform.Translate(Vector3.left * moveVlaue * moveSpeed * Time.deltaTime); //지용님 수정사항 반영
    }

    IEnumerator checkGameState()
    {
        nowGameState = GamePlayManager.Instance.NowGameState;
        Debug.Log(nowGameState);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("checkGameState");
    }
}
