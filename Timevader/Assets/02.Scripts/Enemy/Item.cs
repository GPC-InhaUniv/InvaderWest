using UnityEngine;

public enum ItemList
{
    AddMissileItem = 0,
    RunBarrier,
}

public class Item : MonoBehaviour {
    protected PlayerShip playerShip;
    public ItemList kind;

    float moveSpeed = 3.0f;
    [SerializeField]
    float RotateValue = 1.0f;

    void Start()
    {
        playerShip = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        transform.Rotate(0.0f, RotateValue, 0.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UseItem();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BackGround"))
            ReturnToPool();
    }

    void UseItem()
    {
        Debug.Log("아이템 효과 적용");
        playerShip.GetItem(kind);
        ReturnToPool();
    }

    void ReturnToPool()
    {
        if(kind == ItemList.AddMissileItem)
            PoolController.instance.ReturnToPool(PoolType.Item1Pool, this.gameObject);
        else if(kind == ItemList.RunBarrier)
            PoolController.instance.ReturnToPool(PoolType.Item1Pool, this.gameObject);
    }
}
