using System.Collections;
using UnityEngine;

public class LeftRightMover : MonoBehaviour {
    int moveVlaue = 1;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float leftLimitX = 0, rightLimitX = 0;

    [SerializeField]
    GameState nowGameState;

    void Start()
    {
        StartCoroutine("checkGameState");
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
        transform.Translate(Vector3.left * moveVlaue * moveSpeed * Time.deltaTime);
    }

    IEnumerator checkGameState()
    {
        nowGameState = GamePlayManager.Instance.NowGameState;
        Debug.Log(nowGameState);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("checkGameState");
    }
}
