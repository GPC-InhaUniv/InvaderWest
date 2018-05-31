using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy {
    int maxHp, hp;
    float moveSpeed = 5.0f;
    int seta = 0;
    int curveRate = 6;
    Direction moveDirection;
    public Direction MoveDirection { set { moveDirection = value; } }

    [SerializeField] float fR = 2;
    float MAXRADIAN = 360;
    float MINRADIAN = 0;
    [SerializeField] float fRadian = 0;
    [SerializeField] float fSpeed = 1;   

    void MoveCircle(bool sign)
    {
        //            Circlewise : CounterClockwise 
        seta = (sign)? seta -= 1 : seta += 1;
        if (seta > MAXSETA) seta %= MAXSETA;

        float de2Rad = seta * Mathf.Deg2Rad;
        float sinValue = Mathf.Sin(de2Rad);
        float cosValue = Mathf.Cos(de2Rad);

        float x = cosValue * radius;
        float y = sinValue * radius;
        transform.Translate(new Vector3(x, y, 0) * Time.deltaTime);

        //if (fRadian > MAXRADIAN) fRadian = MINRADIAN;
        //fRadian += fSpeed;
        //// 추가 시킨 각도의 Radian를 구한다. 
        //float deRad = fRadian * Mathf.Deg2Rad; //Radian값으로 Sin과 Cos 값을 구한다. 
        //float sinValue = Mathf.Sin(deRad);
        //float cosValue = Mathf.Cos(deRad);
        ////Debug.Log(fRadian + " Mathf.Sin :" + sinValue + " Mathf.Cos :" + cosValue + deRad);
        //// 반지름을 곱해 포인트 x,y값을 구한다. 
        //float y = 0;
        //float x = 0;
        //y = sinValue * fR;
        //x = cosValue * fR; //이동 
        //transform.Translate(new Vector3(x, y, 0) * Time.deltaTime);
        //transform.localPosition = new Vector3(x, y, 0) + v3MovePos;
    }

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

    override public void Init()
    {
        seta = 0;
        hp = maxHp;
    }
}
