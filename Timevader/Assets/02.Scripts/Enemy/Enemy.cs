using System.Collections;
using System.Collections.Generic;
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
    Normal,
    Wrecked,
    Attacking,
}

/*  FlyWeghit Pattern
 *  GameManager에 배치할 것 */
public class Enemy : MonoBehaviour
{
    public ObjectPool MissilePool;
    public GameObject WreckedShip;
    public GameObject[] Items;
    public EnemyFactory factory;

    [SerializeField]
    protected float moveSpeed, moveHeight, circleSpeed;
    protected float radius;
    
    protected Enemy enemy;
    protected const int MAXSETA = 360;
    protected string[] itemList = { ((ItemList)1).ToString(), ((ItemList)2).ToString() };

    void Awake()
    {
        moveSpeed = 5.0f;
        moveHeight = 6.0f; // Zigzag, Curve에서 사용하는 높이 변화량
        radius = 5.0f; // Circle
        circleSpeed = 4.0f;
        enemy = GameObject.FindWithTag("Factory").GetComponent<Enemy>();
        factory = GetComponent<EnemyFactory>();
    }

    //protected ObjectPool GetMissilePool()
    //{
    //    return MissilePool;
    //}

    virtual protected void Move() { }
    virtual protected void MoveCircle(bool sign) { }
    virtual protected void MoveZigzag(bool sign) { }
    virtual protected void MoveCurve(bool sign) { }
    virtual protected void Init() { }
    virtual protected void GetDemage(int damage) { }

    protected void Explode()
    {
        Debug.Log("EXPLODE");
        if (Random.Range(0, 2) == 0) // 50%
            Wrecked();

        if (Random.Range(0, 5) == 0) // 20%
            DropItem(ItemList.AddMissileItem);
        ReturnToPool();
    }

    void DropItem(ItemList num)
    {
        Debug.Log((int)num);
        Instantiate(Items[0], transform.position, Quaternion.identity);
    }
    /* 우주선이 파괴되면 30% 확률로 잔해가 되어 아래로 점점 떨어진다. 
     * 플레이어에 닿으면 데미지를 준다. 미사일, 플레이어와 충돌 시 파괴 */

    void Wrecked() 
    {
        Debug.Log("WRECKED");
       Instantiate(WreckedShip, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Bolt")
        {
            GetDemage(2); // other.power
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BackGround") ReturnToPool();
    }

    void ReturnToPool()
    {
        // 오브젝트가 화면 밖으로 빠져나가면 false로 변경
        Init();
        this.gameObject.SetActive(false);
    }

    virtual public void SetDirection(Direction dir) { }
}
