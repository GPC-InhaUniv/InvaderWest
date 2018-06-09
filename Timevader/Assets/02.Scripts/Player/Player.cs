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

    public GameObject AddedShot;
    public Transform AddedSpawn;

    public float fireDelta = 0.5f;

    private float nextFire = 0.5f;
    private float myTime = 0.0f;

    Rigidbody rigidbody;
    Vector3 movement;

    public void GetItem(DropItem.ItemKind itemKind)
    {
        switch (itemKind)
        {
            case DropItem.ItemKind.AddMissileitem:
                AddMissile();
                break;
            case DropItem.ItemKind.Assistantitem:
                break;
        }
    }

    void AddMissile()
    {
        Instantiate(Shot, ShotSpawn.position, ShotSpawn.rotation);
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        myTime = myTime + Time.deltaTime;

        if (myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            Instantiate(Shot, ShotSpawn.position, ShotSpawn.rotation);

            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }
    }

    private void FixedUpdate()
    {
        //if (Input.anyKeyDown)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
            rigidbody.velocity = movement * speed;

            rigidbody.position = new Vector3
            (
                Mathf.Clamp(rigidbody.position.x, Boundary.xMin, Boundary.xMax),
                Mathf.Clamp(rigidbody.position.y, Boundary.yMin, Boundary.yMax),
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
