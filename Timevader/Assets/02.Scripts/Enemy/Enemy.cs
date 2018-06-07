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
}

public enum InvaderType
{
    Normal,
    Wrecked,
    Attacking,
}

public enum ItemList
{
    Item1 = 1,
    Item2,
    Item3
}
/*  FlyWeghit Pattern
 *  GameManager에 배치할 것 */
public class Enemy : MonoBehaviour
{
    //public GameObject missile;
    public ObjectPool MissilePool;
    public GameObject WreckedShip;

    protected Enemy enemy;
    protected int MAXSETA = 360;
    protected string[] itemList = { ((ItemList)1).ToString(), ((ItemList)2).ToString() };

    protected int curveRate;
    protected float moveHeight; // Zigzag
    protected float radius; // Circle
    protected float circleSpeed;

    private void Awake()
    {
        curveRate = 6;
        moveHeight = 1f; // Zigzag
        radius = 4f; // Circle
        circleSpeed = 6f;
        enemy = GameObject.FindWithTag("Factory").GetComponent<Enemy>();
    }

    virtual public void Move() { }
    virtual public void MoveCircle(bool sign) { }
    virtual public void Init() { }
    virtual public void GetDemage(int damage) { }
    public void Explode()
    {
        Debug.Log("EXPLODE");
        if (Random.Range(0, 3) == 0)
            Wrecked();
        ReturnToPool();
    }
    public void DropItem()
    {

    }
    /* 우주선이 파괴되면 30% 확률로 잔해가 되어 아래로 점점 떨어진다. 
     * 플레이어에 닿으면 데미지를 준다. 미사일, 플레이어와 충돌 시 파괴 */
    public void Wrecked() 
    {
        Debug.Log("WRECKED");
        Instantiate(WreckedShip, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GetDemage(2); // other.power
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BackGround")
            ReturnToPool();
    }

    public void ReturnToPool()
    {
        // 오브젝트가 화면 밖으로 빠져나가면 false로 변경
        Init();
        this.gameObject.SetActive(false);
    }
}
