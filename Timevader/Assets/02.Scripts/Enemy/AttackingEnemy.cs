using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingEnemy : Enemy{
    int maxHp, hp;
    float attackRate = 0.6f;
    float attackPower = 5.0f;
    float moveSpeed = 5.0f;
    int seta = 0;
    Direction moveDirection;
    public Direction MoveDirection { set { moveDirection = value; } }

    private void Start()
    {
        StartCoroutine(Attack());
        MissilePool = GameObject.FindWithTag("Factory").GetComponent<Enemy>().MissilePool;
    }

    private void FixedUpdate()
    {
        //if (MissilePool == null)
        //{
        //    MissilePool = GameObject.FindWithTag("Factory").GetComponent<Enemy>().MissilePool;
        //    return;
        //}
        Move();
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackRate);
        GameObject shot = MissilePool.GetFromPool();
        if (shot != null)
        {
            shot.transform.position = transform.position;
            shot.transform.rotation = Quaternion.identity;
            shot.SetActive(true);
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
                if (seta > MAXSETA) seta %= MAXSETA;
                seta += 1;
                transform.Translate(new Vector3(Vector3.right.x * moveSpeed, Mathf.Sin(seta / curveRate) * moveSpeed * moveHeight, 0) * Time.deltaTime);
                break;
            case Direction.Zigzag_RightToLeft:
                if (seta > MAXSETA) seta %= MAXSETA;
                seta += 1;
                transform.Translate(new Vector3(Vector3.left.x * moveSpeed, Mathf.Sin(seta / curveRate) * moveSpeed * moveHeight, 0) * Time.deltaTime);
                break;
            case Direction.Circle_Clockwise:
                MoveCircle(true); break;
            case Direction.Circle_CounterClockwise:
                MoveCircle(false); break;
            default: break;
        }
    }

    override public void MoveCircle(bool sign)
    {
        //            Circlewise : CounterClockwise 
        seta = (sign) ? seta -= 1 : seta += 1;
        if (seta > MAXSETA) seta %= MAXSETA;

        float de2Rad = circleSpeed * seta * Mathf.Deg2Rad;
        float sinValue = Mathf.Sin(de2Rad);
        float cosValue = Mathf.Cos(de2Rad);

        float x = cosValue * radius;
        float y = sinValue * radius;
        transform.Translate(new Vector3(x, y, 0) * circleSpeed / 2 * Time.deltaTime);
    }

    override public void Init()
    {
        seta = 0;
        hp = maxHp;
    }
}
