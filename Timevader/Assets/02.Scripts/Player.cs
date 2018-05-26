using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class Player : MonoBehaviour {

    public float speed;
    public Boundary boundary;

    private void FixedUpdate()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        rigidbody.velocity = movement * speed;
        /*
        rigidbody.position = new Vector3
        (
            Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp (rigidbody.position.y, boundary.yMin, boundary.yMax),
            0.0f
        );
        */
    }
}
