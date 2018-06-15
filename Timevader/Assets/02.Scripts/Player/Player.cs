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
    [SerializeField]
    private Transform shotSpawn;
    [SerializeField]
    private Transform addedSpawn;

    [SerializeField]
    private float fireDelta = 0.5f;

    private float nextFire = 0.5f;
    private float myTime = 0.0f;

    Rigidbody rigid;
    Vector3 movement;

    bool hasDoubleMissile = false;

    public void GetItem(ItemList itemKind)
    {
        switch (itemKind)
        {
            case ItemList.AddMissileItem:
                AddMissile();
                break;
            case ItemList.IncreasingShotSpeedItem:
                IncreasingShotSpeed();
                break;
            case ItemList.AssistantItem:
                break;
        }
    }

    void AddMissile()
    {
        hasDoubleMissile = true;
        Shoot(addedSpawn);
    }

    void IncreasingShotSpeed()
    {
        fireDelta = 0.3f;
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
        if (hasDoubleMissile == true)
        {
            Shoot(shotSpawn);
            Shoot(addedSpawn);
        }
        else
            Shoot(shotSpawn);
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
