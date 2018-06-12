﻿using System.Collections;
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

public enum ItemList
{
    Item1 = 0,
    Item2,
    Item3
}
/*  FlyWeghit Pattern
 *  GameManager에 배치할 것 */
public class Enemy : MonoBehaviour
{
    public ObjectPool MissilePool;
    public GameObject WreckedShip;
    public GameObject[] Items;

    protected Enemy enemy;
    protected int MAXSETA = 360;
    protected string[] itemList = { ((ItemList)1).ToString(), ((ItemList)2).ToString() };

    protected int curveRate;
    protected float moveHeight; // Zigzag
    protected float radius; // Circle
    protected float circleSpeed;

    private void Awake()
    {
        curveRate = 3;
        moveHeight = 5f; // Zigzag
        radius = 4f; // Circle
        circleSpeed = 6f;
        enemy = GameObject.FindWithTag("Factory").GetComponent<Enemy>();
    }

    protected ObjectPool GetMissilePool()
    {
        return MissilePool;
    }

    virtual public void Move() { }
    virtual public void MoveCircle(bool sign) { }
    virtual public void Init() { }
    virtual public void GetDemage(int damage) { }

    public void Explode()
    {
        Debug.Log("EXPLODE");
        if (Random.Range(0, 2) == 0) // 50%
            Wrecked();

        if (Random.Range(0, 5) == 0) // 20%
            DropItem(ItemList.Item1);
        ReturnToPool();
    }

    public void DropItem(ItemList num)
    {
        Debug.Log((int)num);
        Instantiate(Items[0], transform.position, Quaternion.identity);
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
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Bolt")
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
