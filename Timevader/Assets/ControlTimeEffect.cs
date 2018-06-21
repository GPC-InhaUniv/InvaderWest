using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTimeEffect : MonoBehaviour {

    TimeEffect EffectPool;

    Transform bossVector3;

    GameObject TargetObject;

    float moveSpeed, rotateSpeed;

    void Start()
    {
        EffectPool = GameObject.Find("EffectPooling").GetComponent<TimeEffect>();

        TargetObject = GameObject.FindWithTag("Boss");

        bossVector3 = TargetObject.transform;

        moveSpeed = Random.Range(1f, 5f);
        rotateSpeed = Random.Range(0.01f, 0.2f);
    }

    void FixedUpdate()
    {
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
            EffectPool.HideEffect(gameObject);
        }
    }

}
