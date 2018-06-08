using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  ObjectPool Pattern
 *  GameManager에 배치할 것 */
public class ObjectPool : MonoBehaviour {

    [SerializeField]
    private GameObject pooledObject;
    [SerializeField]
    private int poolSize = 21;
    [SerializeField]
    private bool canIncreaseSize = true; // pool Size 자동 증가
    List<GameObject> pool; // 여유가 있으면 activeInHieracrchy 쓰지말고 queue로 할 것

    private void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pool.Add(obj);
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
            if(!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }

        if(canIncreaseSize)
        {
            // pool의 오브젝트가 모두 사용중이면 pool 사이즈를 증가시킨다.
            GameObject obj = Instantiate(pooledObject);
            pool.Add(obj);
            return obj;
        }

        Debug.Log("Pool에 남은 enemy가 부족합니다.");
        return null;
    }
}
