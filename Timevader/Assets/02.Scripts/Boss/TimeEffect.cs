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
        spawnPositionVector = spawnPosition.transform.position;
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
        effect = PoolController.instance.GetFromPool(PoolType.DrainPool);

        spawnPositionVector = spawnPosition.transform.position;
        if (effect != null)
        {
            effect.transform.position = spawnPositionVector;
        }
    }

    public void HideEffect(GameObject obj)
    {
        PoolController.instance.ReturnToPool(PoolType.DrainPool, obj);
    }
    IEnumerator checkGameState()
    {
        nowGameState = GamePlayManager.Instance.NowGameState;
        Debug.Log(nowGameState);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("checkGameState");
    }
}
