using UnityEngine;

public enum Direction
{
    Line_LeftToRight,
    Line_RightToLeft,
    Circle_Clockwise, // 특정 좌표를 기준으로 회전
    Circle_CounterClockwise,
    Zigzag_LeftToRight, // 지그재그
    Zigzag_RightToLeft,
    Curve_RightDown, // 곡선,
    Curve_LeftDown,
}

public enum InvaderType
{
    Enemy1, // 기본
    Enemy2, // 체력 많은 적
    Enemy3, // 공격하는 적
}

public class Enemy : MonoBehaviour
{
    public AudioSource destroyAudio;

    [Range(0, 100)]
    public int ItemDropProbability; // 20%

    [SerializeField]
    protected float moveSpeed, moveHeight, circleSpeed;
    protected float radius;
    
    //protected Enemy enemy;
    protected const int MAXSETA = 360;
    protected string[] itemList = { ((ItemList)1).ToString(), ((ItemList)2).ToString() };

    protected static Enemy enemy = null;

    void Awake()
    {
        moveSpeed = 5.0f;
        moveHeight = 6.0f; // Zigzag, Curve에서 사용하는 높이 변화량
        radius = 5.0f; // Circle
        circleSpeed = 4.0f;
        enemy = GameObject.FindWithTag("Factory").GetComponent<Enemy>();
        //Enemy전체 Explode
        PlayerShip.OnUseItem += Explode; 
    }

    virtual protected void Move() { }
    virtual protected void MoveCircle(bool sign) { }
    virtual protected void MoveZigzag(bool sign) { }
    virtual protected void MoveCurve(bool sign) { }
    virtual protected void Init() { }
    virtual protected void GetDemage() { }

    protected void Explode()
    {
        //Explode될때 소리 추가//
        destroyAudio.Play();
        GameObject explosion = PoolController.instance.GetFromPool(PoolType.ExplosionPool);
        if (explosion != null)
            explosion.transform.position = transform.position;

        if (Random.Range(1, 100) <= ItemDropProbability)
            DropItem((ItemList)Random.Range(0, 2));
        ReturnToPool();
    }

    void DropItem(ItemList num)
    {
        Debug.Log("ItemNUmber : " + (int)num);
        GameObject item = null;
        if (num == ItemList.AddMissileItem) item = PoolController.instance.GetFromPool(PoolType.Item1Pool);
        else if (num == ItemList.RunBarrier) item = PoolController.instance.GetFromPool(PoolType.Item2Pool);

        if (item == null)
        {
            Debug.Log("아이템이 없습니다.");
        }
        item.transform.position = transform.position;
        item.transform.rotation = Quaternion.identity;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bolt"))
        {
            GetDemage(); // other.power
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("BackGround")) ReturnToPool();
    }

    void ReturnToPool()
    {
        // 오브젝트가 화면 밖으로 빠져나가면 false로 변경
        Init();
    }

    virtual public void SetDirection(Direction dir) { }
}
