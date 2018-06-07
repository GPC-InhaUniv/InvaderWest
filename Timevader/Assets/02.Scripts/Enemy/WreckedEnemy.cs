using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckedEnemy : Enemy{
    int hp, maxHp = 10;
    float moveSpeed = 3.0f;

    private void Start()
    {
        hp = maxHp;
        WreckedShip = enemy.WreckedShip;
    }
    private void FixedUpdate()
    {
        Move();
    }

    override public void Move()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    override public void GetDemage(int damage)
    {
        hp -= damage;
        Debug.Log(gameObject.name + "Damage " + damage);
        if (hp <= 0) Destroy(gameObject);
    }
}
