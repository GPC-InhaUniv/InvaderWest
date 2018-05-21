using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragRotation : MonoBehaviour {
    public GameObject Earth;

    Vector2 PrevPoint;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) || Input.touchCount == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse Click");
                PrevPoint = Input.mousePosition;
                //new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("touch Click");
                PrevPoint = Input.GetTouch(0).position;
            }

            if (Input.GetMouseButton(0))
            {
                Debug.Log("Mouse Drag");
                //TouchStatus = "Touch Began";

                Earth.transform.Rotate(Input.mousePosition.y - PrevPoint.y, -(Input.mousePosition.x - PrevPoint.x), 0);
                PrevPoint = Input.mousePosition;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Debug.Log("Touch Drag");
                //TouchStatus = "Touch Began";

                Earth.transform.Rotate(Input.GetTouch(0).position.y - PrevPoint.y, -(Input.GetTouch(0).position.x - PrevPoint.x), 0);
                PrevPoint = Input.GetTouch(0).position;
            }
            
        }
        else
        {
            Earth.transform.Translate(0, 0, 0);
            // TouchStatus = "Idle";
        }
	}
}
