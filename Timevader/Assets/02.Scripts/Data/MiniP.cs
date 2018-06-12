using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniP : MonoBehaviour {

    public GameObject Player;


    public GameObject Shot;
    public Transform ShotSpawn;

    public float fireDelta = 0.35f;

    private float nextFire = 0.35f;
    private float myTime = 0.0f;


    Rigidbody rigid;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //고정//
        gameObject.transform.RotateAround(new Vector3( 0f, Player.transform.position.y, 0f),0.1f);


        Shoot(ShotSpawn);

    }

    void Shoot(Transform AnySpawn)
    {
        myTime = myTime + Time.deltaTime;

        if (myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            Instantiate(Shot,new Vector3( AnySpawn.position.x,AnySpawn.position.y,0.0f), Quaternion.identity);

            nextFire = nextFire - myTime;
            myTime = 0.0f;
        }
    }
}
