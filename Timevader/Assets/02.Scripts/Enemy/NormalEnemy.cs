﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy {
    protected int maxHp, hp;
    protected float moveSpeed;
    protected Direction moveDirection;
    public Direction MoveDirection { set { moveDirection = value; } }


    //public override void Move()
    //{
    //    switch(moveDirection)
    //    {
    //        case Direction.Line:
    //            break;
    //        case Direction.Zigzag:
    //            break;
    //        case Direction.Circle:
    //            break;
    //    }
    //}

    //public override void GetDemage()
    //{

    //}

    //public override void Explode()
    //{

    //}

    //public override void DropItem()
    //{

    //}

    //public override void Wrecked()
    //{

    //}
}
