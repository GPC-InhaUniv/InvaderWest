using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTimeEffect : MonoBehaviour {

    TimeEffect effectPool;

    Transform bossVector3;

    GameObject targetObject;
    [SerializeField]
    GameState nowGameState;

    float moveSpeed, rotateSpeed;

    void Start()
    {
        effectPool = GameObject.Find("TimeEffectPooling").GetComponent<TimeEffect>();

        targetObject = GameObject.FindGameObjectWithTag("Boss");
        bossVector3 = targetObject.transform;

        moveSpeed = Random.Range(1f, 5f);
        rotateSpeed = Random.Range(0.01f, 0.2f);
        nowGameState = GamePlayManager.Instance.NowGameState;
        GamePlayManager.OnChangeGamestate += CheckGameState;
    }

    void FixedUpdate()
    {
        if (nowGameState == GameState.Started)
            MoveEffect();
    }

    public void MoveEffect()
    {
        Vector3 targetVector = bossVector3.position - transform.position;

        Quaternion targetQuaternion = Quaternion.LookRotation(targetVector);

        Quaternion SlerpTargetQuaternion = Quaternion.Slerp(transform.rotation, targetQuaternion, rotateSpeed + Time.deltaTime);

        transform.rotation = SlerpTargetQuaternion;

        transform.Translate(new Vector3(0, 0, 1) * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("이펙트 충돌");
        if (other.gameObject.tag == "Boss")
        {
            effectPool.HideEffect(gameObject);
        }
    }
    public void CheckGameState()
    {
        nowGameState = GamePlayManager.Instance.NowGameState;
    }
}
