using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnEffectToPool : MonoBehaviour {

    [SerializeField]
    float DELAYTIME = 0.5f;

    IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(DELAYTIME);
        PoolController.instance.ReturnToPool(PoolType.ExplosionPool, this.gameObject);
        init();
        //gameObject.SetActive(false);
    }

    void init()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // 이펙트 크기를 원래대로
    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine("ReturnToPool");
    }
}
