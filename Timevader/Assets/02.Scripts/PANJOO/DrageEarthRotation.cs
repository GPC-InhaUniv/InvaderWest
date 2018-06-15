using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DrageEarthRotation : MonoBehaviour {

    float middleValue = 100.0f;
    float increaseValue = 1.0f;

    //지구를 돌리는
    [SerializeField]
    GameObject Earth;

    [SerializeField]
    GameObject Cloud;
    Vector3 prevPoint;

    float ConditionRotation;
    
    [SerializeField]
    [Range(-0.5f,5f)]
    float rotateSpeed;

    [SerializeField]
    Camera MainCamera;

    UIFader uIFader;
    [SerializeField]
    CanvasGroup FadePanel;

    [SerializeField] //로그인 조건 만족시 사라짐
    GameObject[] otherObject;

    void FixedUpdate()
    {
        ActionClickOrDarg();
        NextScene();
    }
    void Start()
    {
        ConditionRotation = 0.90f;
        uIFader = GetComponent<UIFader>();
    }

    void ActionClickOrDarg()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("클릭");

            if (Physics.Raycast(ray, out hit))
            {
                // 지구 회전
                Debug.Log("Raycast Hit");
                TouchSlide();
            }
        }
    }

    void TouchSlide()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
            prevPoint = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Drag");
            float dragValue = (prevPoint.x - Input.mousePosition.x) / 5 - (prevPoint.y - Input.mousePosition.y);
            // r^2 = x^2 + y^2
            Vector3 rotatePower = new Vector3(0, 0, dragValue);

            //Debug.Log("roat" + dragValue);
            Cloud.transform.Rotate(rotatePower / 2 * rotateSpeed);
            Earth.transform.Rotate(rotatePower / 2 * rotateSpeed);
            prevPoint = Input.mousePosition;
        }
    }

    void NextScene()
    {
        Debug.Log(Earth.transform.rotation.z);
        if(Earth.transform.rotation.z < -ConditionRotation || Earth.transform.rotation.z > ConditionRotation)
        {
            Debug.Log("다음 씬으로");
            StartCoroutine(EffectCoroutine());
<<<<<<< HEAD
            {
                if (MainCamera.fieldOfView < 170.0f)
                {
                    HieObject();
                    uIFader.CanvasFadeIn(FadePanel);
                    StartCoroutine(WaitTimeForNextScene());
                }
            }
=======

            if (MainCamera.fieldOfView < 170.0f)
                HieObject();
                uIFader.CanvasFadeIn(FadePanel);

                StartCoroutine(WaitTimeForNextScene());
>>>>>>> 06cd94e889265949dc9b49c15c1241f48f762208
        }
    }

    void HieObject()
    {
        for (int i = 0; i < otherObject.Length-1; i++)
        {
            otherObject[i].SetActive(false);
        }
        Earth.SetActive(false);
        Cloud.SetActive(false);
    }

    IEnumerator EffectCoroutine()
    {
        Debug.Log("씬 넘어가는 효과 코루틴");
        if (MainCamera.fieldOfView < 177.0f)
        {
            NextEffect();
        }
        yield return new WaitForSeconds(2.0f);
    }

    IEnumerator WaitTimeForNextScene()
    {
        Debug.Log("다음 씬 넘어가는 코루틴");
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Main");
    }

    void NextEffect()
    {
        MainCamera.fieldOfView += increaseValue;

        if (MainCamera.fieldOfView > middleValue)
            increaseValue = 77.0f;        
    }
}
