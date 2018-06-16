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
    float speed;

    [SerializeField]
    Boundary Boundary;

    public GameObject Shot;
    [SerializeField]
    Transform shotSpawn, addedSpawn;

    [SerializeField]
    float fireDelta = 0.5f;

    float nextFire = 0.5f;
    float myTime = 0.0f;

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

    void Update()
    {
        if (hasDoubleMissile == true)
        {
            Shoot(shotSpawn);
            Shoot(addedSpawn);
        }
        else
            Shoot(shotSpawn);
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    /* 지용 */
    void MovePlayer()
    {
        if (Input.GetMouseButton(0))
        {
            movement = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));            
            transform.position = Vector3.Lerp(transform.position, movement, Time.deltaTime * speed);

            transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, Boundary.xMin, Boundary.xMax),
                Mathf.Clamp(transform.position.y, Boundary.yMin, Boundary.yMax),
                0.0f
            );
        }
    }
}
