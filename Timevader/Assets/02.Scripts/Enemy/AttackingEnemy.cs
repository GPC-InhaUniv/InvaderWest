using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingEnemy : Enemy{
    int maxHp = 3, hp;
    float moveSpeed = 2.0f;
    float seta = 0;
    float setaRate = 0.5f;
    Direction moveDirection;
    public Direction MoveDirection { set { moveDirection = value; } }
    int attackRate = 11;
    //float attackPower = 1.0f;

    private void Start()
    {
        
        hp = maxHp;
        MissilePool = enemy.MissilePool;
        WreckedShip = enemy.WreckedShip;
        Items = enemy.Items;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds((float)Random.Range(5, attackRate) / 10);
            
            GameObject shot = MissilePool.GetFromPool();
            if (shot != null)
            {
                shot.transform.rotation = Quaternion.identity;
                shot.SetActive(true);
                shot.transform.position = transform.position;
            }
            yield return null;

        }
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
                MoveZigzag(true); break;

            case Direction.Zigzag_RightToLeft:
                MoveZigzag(false); break;

            case Direction.Circle_Clockwise:
                MoveCircle(true); break;

            case Direction.Circle_CounterClockwise:
                MoveCircle(false); break;

            case Direction.Curve_RightDown:
                MoveCurve(); break;

            case Direction.Curve_LeftDown:
                break;
            default: break;
        }
    }

    public void MoveCurve()
    {
        //transform.Translate(Vector3.right.x * moveSpeed *3, 0) * Time.deltaTime);

    }

    // TRUE : LeftToRight, FALSE : RightToLeft
    public void MoveZigzag(bool sign)
    {
        if (seta > MAXSETA) seta %= MAXSETA;
        seta += setaRate;
        if(sign) transform.Translate(new Vector3(Vector3.right.x * moveSpeed, Mathf.Sin(seta / curveRate) * moveSpeed * moveHeight, 0) * Time.deltaTime);
        else transform.Translate(new Vector3(Vector3.left.x * moveSpeed, Mathf.Sin(seta / curveRate) * moveSpeed * moveHeight, 0) * Time.deltaTime);
    }
    
    override public void MoveCircle(bool sign)
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

    override public void Init()
    {
        seta = 0;
        hp = maxHp;
    }

    override public void GetDemage(int damage)
    {
        hp -= damage;
        //Debug.Log(gameObject.name + "Damage " + damage);
        if (hp <= 0) Explode();
    }
}
