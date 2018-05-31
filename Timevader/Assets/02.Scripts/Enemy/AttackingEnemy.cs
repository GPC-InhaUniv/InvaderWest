using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingEnemy : Enemy{
    int maxHp, hp;
    float attackRate = 10.0f;
    float attackPower = 5.0f;
    float moveSpeed = 5.0f;
    int seta = 0;
    int curveRate = 6;
    Direction moveDirection;
    public Direction MoveDirection { set { moveDirection = value; } }

    private void FixedUpdate()
    {
        Move();
    }

    override public void Move()
    {
        switch (moveDirection)
        {
            case Direction.Line_LeftToRight:
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                break;
            case Direction.Line_RightToLeft:
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                break;
            case Direction.Zigzag_LeftToRight:
                seta += 1;
                transform.Translate(new Vector3(Vector3.right.x * moveSpeed, Mathf.Sin(seta / curveRate) * moveSpeed * moveHeight, 0) * Time.deltaTime);
                break;
            case Direction.Zigzag_RightToLeft:
                seta += 1;
                transform.Translate(new Vector3(Vector3.left.x * moveSpeed, Mathf.Sin(seta / curveRate) * moveSpeed * moveHeight, 0) * Time.deltaTime);
                break;
            case Direction.Circle_Clockwise:
                
                break;
            case Direction.Circle_CounterClockwise:
                break;
            default: break;
        }
    }

    override public void Init()
    {
        seta = 0;
        hp = maxHp;
    }
}
