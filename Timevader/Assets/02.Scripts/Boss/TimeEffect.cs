using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEffect : MonoBehaviour {

    public BossEffectPoolObject EffectPool;

    [SerializeField]
    GameObject spawnPosition;

    [SerializeField]
    GameState nowGameState;

    GameObject effect;

    Vector3 spawnPositionVector;

    void Start()
    {
        StartCoroutine("checkGameState");
        spawnPositionVector = spawnPosition.transform.position; // 스폰위치에 생성
    }

    void Update()
    {
        if (nowGameState == GameState.Started)
        {
            ActiveEffect();
        }
    }

    public void ActiveEffect()
    {
        effect = EffectPool.GetPooledObject(); //de

        spawnPositionVector = spawnPosition.transform.position;

        if (effect != null)
        {
            effect.transform.position = spawnPositionVector;
            effect.SetActive(true);
        }

    }

    public void HideEffect(GameObject obj)
    {
        Debug.Log("숨기기");
        EffectPool.ReturnToPool(obj); //en
    }
    IEnumerator checkGameState()
    {
        nowGameState = GamePlayManager.Instance.NowGameState;
        Debug.Log(nowGameState);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("checkGameState");
    }
}
