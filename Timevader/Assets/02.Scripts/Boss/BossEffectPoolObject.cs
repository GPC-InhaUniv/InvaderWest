using System.Collections.Generic;
using UnityEngine;

public class BossEffectPoolObject : MonoBehaviour {

    [SerializeField]
    GameObject PoolObjEffect;

    [SerializeField]
    GameObject SpawnObj;

    [SerializeField]
    int PoolAmount = 10;

    Queue<GameObject> PoolObjsEffect;


    void Start()
    {
        CreateEffectPool();
    }

    void CreateEffectPool()
    {
        PoolObjsEffect = new Queue<GameObject>();

        for (int i = 0; i < PoolAmount; i++)
        {
            GameObject Effectobj = Instantiate(PoolObjEffect);

            Effectobj.transform.parent = SpawnObj.transform;

            Effectobj.SetActive(false);

            PoolObjsEffect.Enqueue(Effectobj);
        }
        Debug.Log("objpool 이펙트 생성 완료 ");
    }

    public GameObject GetPooledObject()
    {
        if(PoolObjsEffect.Count != 0)
            return PoolObjsEffect.Dequeue();

        Debug.Log("Pool에 이펙트가 없음.");

        return null;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
<<<<<<< HEAD
=======


        PoolObjsEffect.Enqueue(obj);
        
>>>>>>> d9bf4262da837228b84a2e47c9b2bc1d4e629750
    }
}
