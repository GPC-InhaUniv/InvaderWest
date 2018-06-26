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

    int logCount = 500;
    string testStr;

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    void DebugLogWarp(string str)
    {
        Debug.Log("str");
    }

    void Update()
    {
        for (int i =0; i<logCount; i++)
        {
            DebugLogWarp("TestLog : " + testStr);
        }
    }

    [System.Diagnostics.Conditional("UNITY_ANDROID"), System.Diagnostics.Conditional("UNITY_EDITOR")]
    void PlayOnAndroid()
    {
        Debug.Log("안드로이드 또는 에디터에서 플레이시 호출되는 함수");
    }

    [System.Diagnostics.Conditional("UNITY_ANDROID")]
    void PlayOnAndroidOREditor()
    {
        Debug.Log("안드로이드 플랫폼에서만 호출되는 함수, ex. 터치");
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

        PoolObjsEffect.Enqueue(obj);

        obj.SetActive(false);


        
    }

     
}
