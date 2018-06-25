using UnityEngine;

public class EnemyFactory : MonoBehaviour {
    public static EnemyFactory instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public GameObject GetEnemy(InvaderType type)
    {
        GameObject invader = null;
        switch(type)
        {
            case InvaderType.Normal:
                invader = PoolController.instance.GetFromPool(PoolType.NormalPool); break;
            case InvaderType.Attacking:
                invader = PoolController.instance.GetFromPool(PoolType.AttackingPool); break; 
            default: break;
        }
        return invader;
    }
}