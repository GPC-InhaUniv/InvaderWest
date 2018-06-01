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

    public float fireDelta = 0.5f;

    private float nextFire = 0.5f;
    private float myTime = 0.0f;

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
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        rigidbody.velocity = movement * speed;
        
        rigidbody.position = new Vector3
        (
            Mathf.Clamp (rigidbody.position.x, Boundary.xMin, Boundary.xMax),
            Mathf.Clamp (rigidbody.position.y, Boundary.yMin, Boundary.yMax),
            -328.5f
        );
        
    }
}
