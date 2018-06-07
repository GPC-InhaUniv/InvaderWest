using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {

    [Range(0, 5)]
    public float movement;
    public  int a = 1;


    public void Update()
    {
        if (transform.localPosition.x < -3.0f)
        {
            a = -1;
        }
        else if(transform.localPosition.x > 2.0f)
        {
            a = 1;
        }
        transform.Translate(Vector3.left * 1.0f * Time.deltaTime * a);
    }
}
