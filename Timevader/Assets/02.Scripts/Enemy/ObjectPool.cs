using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  ObjectPool Pattern
 *  GameManager에 배치할 것 */
public class ObjectPool : MonoBehaviour {

    public string TargetObject = string.Empty; // ObjectPool 구분을 위해 오브젝트 표기를 위한 변수

    [SerializeField]
    GameObject pooledObject;
    [SerializeField]
    int poolSize = 21;
    Queue<GameObject> pool; // 여유가 있으면 activeInHieracrchy 쓰지말고 queue로 할 것

    void Start()
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

    public GameObject GetFromPool()
    {
        if (pool.Count != 0)
            return pool.Dequeue();
        Debug.Log("Pool에 남은 enemy가 부족합니다.");
        return null;
    }

    public void ReturnToPool(GameObject obj)
    {
        pool.Enqueue(obj);
    }
}
