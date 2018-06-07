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

    protected int MAXSETA = 360;
    protected string[] itemList = { ((ItemList)1).ToString(), ((ItemList)2).ToString() };

    protected int curveRate;
    protected float moveHeight; // Zigzag
    protected float radius; // Circle
    protected float circleSpeed;

    private void Awake()
    {
        //missilePool = gameObject.AddComponent<ObjectPool>();
        //missilePool = gameObject.AddComponent("ObjectPool") as ObjectPool); // 스크립트 컴포넌트 추가안됨?? 어떻게 추가하지?
        //missilePool.SetObject(missile);

        curveRate = 6;
        moveHeight = 1f; // Zigzag
        radius = 4f; // Circle
        circleSpeed = 6f;
    }

    virtual public void Move() { }
    virtual public void MoveCircle(bool sign) { }
    virtual public void Init() { }
    public void GetDemage()
    {

    }
    public void Explode()
    {

    }
    public void DropItem()
    {

    }
    /* 우주선이 파괴되면 30% 확률로 잔해가 되어 아래로 점점 떨어진다. 
     * 플레이어에 닿으면 데미지를 준다. 미사일, 플레이어와 충돌 시 파괴 */
    public void Wrecked() 
    {

    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BackGround")
            OutofScreen();
    }

    public void OutofScreen()
    {
        // 오브젝트가 화면 밖으로 빠져나가면 false로 변경
        Init();
        this.gameObject.SetActive(false);
    }
}
