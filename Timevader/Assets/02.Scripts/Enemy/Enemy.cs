using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Line,
    Circle, // 특정 좌표를 기준으로 회전
    Zigzag, // 지그재그
    //diagonal Line // 대각선
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
public abstract class Enemy : MonoBehaviour
{
    int hp;
    float moveSpeed;
    string[] itemList = { ((ItemList)1).ToString(), ((ItemList)2).ToString() };
    //float attackRate; // Attacking만

    public abstract void Move();
    //public abstract void Attack(); // Attacking만
    public abstract void GetDemage();
    public abstract void Explode();
    public abstract void DropItem();
    public abstract void Wrecked(); /* 우주선이 파괴되면 잔해가 되어 아래로 점점 떨어진다. 
                                     * 플레이어나 우주선에 닿으면 데미지를 준다. 파괴되지 않는다. */

    public void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "BackGround")
        {
            this.gameObject.SetActive(false);
        }
    }
}
