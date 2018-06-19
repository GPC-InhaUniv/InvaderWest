using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffectPoolObject : MonoBehaviour {

    public string PoolItemName = string.Empty;
    [SerializeField]
    GameObject PoolObjEffect;
    [SerializeField]
    GameObject BossObj;

    [SerializeField]
    int PoolAmount = 20;

    public static Queue<GameObject> PoolObjsEffect;


    void Start()
    {
        CreateEffectPool();
    }

    void CreateEffectPool()
    {
        PoolObjsEffect = new Queue<GameObject>();

        for (int i = 0; i < PoolAmount; i++)
        {
            GameObject obj_A = Instantiate(PoolObjEffect);

            obj_A.transform.parent = BossObj.transform;

            obj_A.SetActive(false);

            PoolObjsEffect.Enqueue(obj_A);
        }
        Debug.Log("objpool 이펙트 생성 완료 ");
    }

    public GameObject GetPooledObject()
    {
        if (PoolObjsEffect.Count != 0)
            return PoolObjsEffect.Dequeue();

        Debug.Log("Pool에 이펙트가 없음.");

        return null;
    }

    public void ReturnToPool(GameObject obj)
    {
        PoolObjsEffect.Enqueue(obj);
    }
}
