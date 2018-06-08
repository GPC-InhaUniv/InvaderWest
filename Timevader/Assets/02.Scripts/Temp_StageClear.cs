using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temp_StageClear : MonoBehaviour {
    [SerializeField]
    private Camera mainCam;

    Vector3 startPos, endPos;
    public bool gameClear;

    private void Update()
    {
        if (mainCam.transform.eulerAngles.y == 90.0f) gameClear = false;//LoadStage();
    }

    private void LateUpdate()
    {
        if(gameClear) MoveCam();
    }

    public void MoveCam()
    {
        mainCam.transform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime);
        mainCam.transform.Rotate(0, Mathf.Lerp(0f, 90f, Time.deltaTime), 0);
        
        //mainCam.transform.Rotate(Vector3.Lerp(new Vector3(0,0,0), new Vector3(0, 90f, 0), Time.deltaTime));

        
    }

    public void GameClear()
    {
        startPos = mainCam.transform.position;
        //startPos = new Vector3(0f, 0f, -10f);
        endPos = new Vector3(-10f, 0f, 0f);

        gameClear = true;

        Debug.Log(startPos + " " + endPos);
    }

    public void LoadStage()
    {
        SceneManager.LoadScene("StageSelect");
    }
}
