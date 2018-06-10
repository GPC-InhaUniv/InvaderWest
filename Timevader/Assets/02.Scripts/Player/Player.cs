using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class Player : MonoBehaviour {

    [SerializeField]
    private float speed;

    public Boundary Boundary;

    public GameObject Shot;
    public Transform ShotSpawn;
    public Transform AddedSpawn;

    public float fireDelta = 0.5f;

    private float nextFire = 0.5f;
    private float myTime = 0.0f;

    Rigidbody rigid;
    Vector3 movement;

    public void GetItem(Item.ItemKind itemKind)
    {
        switch (itemKind)
        {
            case Item.ItemKind.AddMissileitem:
                AddMissile();
                break;
            case Item.ItemKind.Assistantitem:
                break;
        }
    }

    void AddMissile()
    {
        Shoot(AddedSpawn);
    }

    void Shoot(Transform AnySpawn)
    {
        myTime = myTime + Time.deltaTime;

        if (myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            Instantiate(Shot, AnySpawn.position, AnySpawn.rotation);

            nextFire = nextFire - myTime;
            myTime = 0.0f;
        }
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Shoot(ShotSpawn);
    }

    private void FixedUpdate()
    {
        //if (Input.anyKeyDown)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
            rigid.velocity = movement * speed;

            rigid.position = new Vector3
            (
                Mathf.Clamp(rigid.position.x, Boundary.xMin, Boundary.xMax),
                Mathf.Clamp(rigid.position.y, Boundary.yMin, Boundary.yMax),
                0.0f
            );
        }
        /*
        else
        {
            movement = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(movement.x, movement.y, 0.0f);
        }
        */
    }
}
