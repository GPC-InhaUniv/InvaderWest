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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BackGround")
            OutofScreen();
    }

    public void OutofScreen()
    {
        // 오브젝트가 화면 밖으로 빠져나가면 false로 변경
        this.gameObject.SetActive(false);
    }

}
