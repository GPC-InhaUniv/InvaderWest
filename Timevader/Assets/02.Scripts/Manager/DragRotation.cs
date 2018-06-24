using UnityEngine;
using UnityEngine.UI;

public class DragRotation : MonoBehaviour {

    public GameObject Earth, Cloud;
    public Text StageInfo;
    public GameObject[] Invaders;
    Vector3 prevPoint;

    public float RotateSpeed = 1f;

    // Update is called once per frame
    void FixedUpdate() {
        if (Input.GetMouseButtonDown(0))
            prevPoint = Input.mousePosition;

        else if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "TouchAble")
            {
                //// 지구 회전
                //Debug.Log(hit.collider.tag);
                //Debug.Log("Raycast Hit");
                TouchSlide();
            }
        }
        else if (Input.GetMouseButtonUp(0))
            prevPoint = Input.mousePosition;
    }

    void TouchSlide()
    {
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("Drag");
            float dragValue = Input.mousePosition.y - prevPoint.y;
            Vector3 rotatePower = new Vector3(0, 0, dragValue );

            //Debug.Log("rota" + dragValue);
            Earth.transform.Rotate(rotatePower / 2 * RotateSpeed);
            Cloud.transform.Rotate(rotatePower / 2 * RotateSpeed / 2); // 느리게 회전
            for (int i = 0; i < Invaders.Length; i++)
            {
                Invaders[i].transform.Rotate(-1 * rotatePower / 2 * RotateSpeed); // 반대로 회전
            }
            prevPoint = Input.mousePosition;
        }
    }
}
