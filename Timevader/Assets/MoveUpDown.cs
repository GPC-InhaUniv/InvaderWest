using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour {

	
	void Start ()
    {
        StartCoroutine(UpDownSpaceShipEffect());
    }
	
    void FixedUpdate()
    {
        
    }

    IEnumerator UpDownSpaceShipEffect()
    {

        Debug.Log("위아래로");
        float a = 0.1f;

        if (transform.localPosition.y < 2.0f)
        {
            a = 0.1f;
        }
        else if (transform.localPosition.y > 1.5f)
        {
            a = -0.1f;
        }

        transform.Translate(new Vector3(0, a, 0));

        yield return new WaitForSeconds(0.2f);
        StartCoroutine(UpDownSpaceShipEffect());
    }
}
