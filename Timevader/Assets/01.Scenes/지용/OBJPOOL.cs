using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJPOOL : MonoBehaviour {

    [SerializeField]
    private GameObject pooledObject;
    [SerializeField]
    private int poolSize = 21;
    Queue<GameObject> pool;

    private void Start()
    {
        CreatePool();
    }

    void CreatePool()
    {
        pool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
        //Debug.Log("pool 생성 완료");
    }

    public void SetObject(GameObject obj)
    {
        pooledObject = obj;
    }

    public GameObject GetFromPool()
    {
        //Debug.Log("GetFromPool 실행");
        for (int i = 0; i < poolSize; i++)
        {
            return pool.Dequeue();
        }
        Debug.Log("Pool에 남은 enemy가 부족합니다.");
        return null;
    }

    public void ReturnToPool(GameObject obj)
    {
        pool.Enqueue(obj);
    }
}
