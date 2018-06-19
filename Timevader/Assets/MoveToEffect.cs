using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToEffect : MonoBehaviour {

    Vector3 spawnPos_Vec_A;
    [SerializeField]
    GameObject spawnPos_A;
    GameObject effect_a;
    
    private void Start()
    {
        spawnPos_Vec_A = spawnPos_A.transform.position;
    }

    void ActiveEffect()
    {
        effect_a = BossEffectPoolObject.PoolObjsEffect.Dequeue();

        if (effect_a == null) return;

        effect_a.transform.position = spawnPos_Vec_A;
        effect_a.SetActive(true);
    }

    public void HideEffect()
    {
        Debug.Log("숨기기");

        BossEffectPoolObject.PoolObjsEffect.Enqueue(effect_a);

        effect_a.SetActive(false);
    }

    public void BtnActiveEffect()
    {
        Debug.Log("나타내기");

        ActiveEffect();
    }
}
