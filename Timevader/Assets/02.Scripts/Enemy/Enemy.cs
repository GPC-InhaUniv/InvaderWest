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
    protected int hp;
    protected float moveSpeed;
    protected Direction moveDirection;
    public Direction MoveDirection { set { moveDirection = value; } }
    protected string[] itemList = { ((ItemList)1).ToString(), ((ItemList)2).ToString() };
    //float attackRate; // Attacking만

    public void Move()
    {

    }
    //public abstract void Attack(); // Attacking만
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

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "BackGround")
        {
            OutofScreen();
        }
    }

    public void OutofScreen()
    {
        // 오브젝트가 화면 밖으로 빠져나가면 false로 변경
        this.gameObject.SetActive(false);
    }
}
