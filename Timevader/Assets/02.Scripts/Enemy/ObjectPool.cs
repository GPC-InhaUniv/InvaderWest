using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  ObjectPool Pattern
 *  GameManager에 배치할 것 */
public class ObjectPool : MonoBehaviour {

    public GameObject PooledObject;
    public int PoolSize = 21;
    public bool CanIncreaseSize = true; // pool Size 자동 증가
    List<GameObject> pool; // 여유가 있으면 activeInHieracrchy 쓰지말고 queue로 할 것

    private void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject obj = Instantiate(PooledObject);
            obj.SetActive(false);
            pool.Add(obj);
        }
        Debug.Log("pool 생성 완료");

    }

    public void SetObject(GameObject obj)
    {
        PooledObject = obj;
    }

    public GameObject GetFromPool()
    {
        Debug.Log("GetFromPool 실행");
        for (int i = 0; i < PoolSize; i++)
        {
            if(!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }

        if(CanIncreaseSize)
        {
            // pool의 오브젝트가 모두 사용중이면 pool 사이즈를 증가시킨다.
            GameObject obj = Instantiate(PooledObject);
            pool.Add(obj);
            return obj;
        }

        Debug.Log("Pool에 남은 enemy가 부족합니다.");
        return null;
    }
}
