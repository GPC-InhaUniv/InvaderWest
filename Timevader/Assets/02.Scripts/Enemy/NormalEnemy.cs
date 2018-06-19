using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy {
    int maxHp = 3, hp;
    float seta = 0;
    float t = 0;
    Direction moveDirection;
    public Direction MoveDirection { set { moveDirection = value; } }

    void Start()
    {
        hp = maxHp;
        WreckedShip = enemy.WreckedShip;
        Items = enemy.Items;
        factory = enemy.factory;
    }

    void FixedUpdate()
    {
        Move();
    }

    override protected void Move()
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
                MoveZigzag(true); break;

            case Direction.Zigzag_RightToLeft:
                MoveZigzag(false); break;

            case Direction.Circle_Clockwise:
                MoveCircle(true); break;

            case Direction.Circle_CounterClockwise:
                MoveCircle(false); break;

            case Direction.Curve_RightDown:
                MoveCurve(true); break;

            case Direction.Curve_LeftDown:
                MoveCurve(true); break;
            default: break;
        }
    }

    // TRUE : RightDown, FALSE : LeftDown
    override protected void MoveCurve(bool sign)
    {
        t += 0.7f * Time.deltaTime;
        if (sign) transform.Translate(new Vector3(Vector3.right.x * moveSpeed, t * Mathf.Lerp(0, moveHeight, t), 0) * Time.deltaTime);
        else transform.Translate(new Vector3(Vector3.right.x * moveSpeed, t * Mathf.Lerp(0, moveHeight, t), 0) * Time.deltaTime);
    }

    // TRUE : LeftToRight, FALSE : RightToLeft
    override protected void MoveZigzag(bool sign)
    {
        if (seta > MAXSETA) seta %= MAXSETA;
        //seta += setaRate;
        seta += Time.deltaTime * moveSpeed;
        if (sign) transform.Translate(new Vector3(Vector3.right.x * moveSpeed, Mathf.Sin(seta) * moveHeight, 0) * Time.deltaTime);
        else transform.Translate(new Vector3(Vector3.left.x * moveSpeed, Mathf.Sin(seta) * moveHeight, 0) * Time.deltaTime);
    }

    // TRUE : Clockwise, FALSE : CounterClockwise
    override protected void MoveCircle(bool sign)
    {
        seta -= 1;
        if (seta > MAXSETA) seta %= MAXSETA;

        float deg2Rad = circleSpeed * seta * Mathf.Deg2Rad;
        float sinValue = Mathf.Sin(deg2Rad);
        float cosValue = Mathf.Cos(deg2Rad);

        float y = sinValue * radius;
        float x = cosValue * radius;
        if (!sign) x *= -1; // CounterClockwise 

        transform.Translate(new Vector3(x, y, 0) * circleSpeed / 2 * Time.deltaTime);
    }

    override protected void Init()
    {
        seta = 0;
        hp = maxHp;
        t = 0;
        factory.normalPool.ReturnToPool(this.gameObject);
    }

    override protected void GetDemage(int damage)
    {
        hp -= damage;
        //Debug.Log(gameObject.name + "Damage " + damage);
        if (hp <= 0) Explode();
    }

    override public  void SetDirection(Direction dir)
    {
        moveDirection = dir;
    }
}
