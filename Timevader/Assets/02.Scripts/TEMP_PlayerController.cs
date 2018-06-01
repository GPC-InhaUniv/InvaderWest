using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_PlayerController : MonoBehaviour {

    public float speed = 10.0f;
    public Bounadry boundary;

    Rigidbody rig;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.25f;
    float nextFire = 0.0f;

    private void Start()
    {
        rig = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, Quaternion.identity);
        }
    }

    Vector3 movement;
    Vector3 privPosition, nowPosition;
    public float gap = 40f;
    private void FixedUpdate()
    {
        //if(Input.anyKeyDown)
        {
            Debug.Log("KeyMove");
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

            rig.velocity = movement * speed;

            rig.position = new Vector3(
                Mathf.Clamp(rig.position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(rig.position.y, boundary.yMin, boundary.yMax),
                0.0f
                );
            //rig.rotation = Quaternion.Euler(0.0f, 0.0f, rig.velocity.x * -tilt);
        }
        //else
        //{
        //    Debug.Log("Drag");
        //    movement = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    transform.position = new Vector3(movement.x, 0.0f, movement.z);
        //    rig.rotation = Quaternion.Euler(0.0f, 0.0f, rig.velocity.x * -tilt);
        //}
    }
}
[System.Serializable]
public class Bounadry
{
    public float xMin, xMax, yMin, yMax;
}