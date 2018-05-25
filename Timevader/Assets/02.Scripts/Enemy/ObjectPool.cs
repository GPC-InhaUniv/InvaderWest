using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  ObjectPool Pattern
 *  GameManager에 배치할 것 */
public class ObjectPool : MonoBehaviour {

    public GameObject PooledObject;
    public int PoolCount = 28;
    List<GameObject> pool;

    private void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < PoolCount; i++)
        {
            GameObject obj = Instantiate(PooledObject);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetFromPool()
    {
        for (int i = 0; i < PoolCount; i++)
        {
            if(!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        Debug.Log("Pool에 남은 enemy가 부족합니다.");
        return null;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

}
