using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckedEnemy : Enemy{
    int maxHp = 10, hp;

    void Start()
    {
        hp = maxHp;
        destroyAudio = enemy.destroyAudio;
        //WreckedShip = enemy.WreckedShip;
    }

    void FixedUpdate()
    {
        Move();
    }

    override protected void Move()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    override protected void GetDemage()
    {
        hp -= 1;
        //Debug.Log(gameObject.name + "Damage " + damage);
        if (hp <= 0) Destroy(gameObject);
    }
}
