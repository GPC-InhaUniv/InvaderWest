using UnityEngine;

public enum ItemList
{
    AddMissileItem = 0,
    RunBarrier,
}

public class Item : MonoBehaviour {
    public ItemList kind;

    PlayerShip playerShip;
    GameState nowGameState;
    float moveSpeed = 3.0f;
    float RotateValue = 4.0f;
    
    void Start()
    {
        GamePlayManager.OnChangeGamestate += CheckGameState;
        playerShip = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        transform.Rotate(0.0f, RotateValue, 0.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || nowGameState == GameState.Started)
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

    public void CheckGameState()
    {
        nowGameState = GamePlayManager.Instance.NowGameState;
    }
}
