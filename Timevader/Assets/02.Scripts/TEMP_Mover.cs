using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_Mover : MonoBehaviour {

    public float speed = -10.0f;
    Rigidbody rig;

	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody>();
        rig.velocity = speed * Vector3.down;
	}

}
