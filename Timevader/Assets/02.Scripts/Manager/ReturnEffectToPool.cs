using System.Collections;
using UnityEngine;

public class ReturnEffectToPool : MonoBehaviour {

    [SerializeField]
    float DELAYTIME = 0.3f;
    [SerializeField]
    bool isHit;

    IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(DELAYTIME);
        if(isHit)
            PoolController.instance.ReturnToPool(PoolType.HitEffectPool, this.gameObject);
        else
            PoolController.instance.ReturnToPool(PoolType.ExplosionPool, this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine("ReturnToPool");
    }
}
