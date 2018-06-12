using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temp_StageClear : MonoBehaviour {
    [SerializeField]
    private GameObject mainCam, clearInfoPanel;

    public bool gameClear;
    //float seta = 0;

    private void FixedUpdate()
    {
        //if (mainCam.transform.eulerAngles.y == 270.0f)
        //{
        //    gameClear = false;
        //}
        if (gameClear) MoveCam();
    }

    int speed = 0;
    public void MoveCam()
    {
        //if (mainCam.transform.eulerAngles.y == 270.0f) return;
        mainCam.transform.Rotate(0, Mathf.Lerp(0f, -90f, Time.deltaTime * 1.0f), 0);
        mainCam.transform.Translate(Vector3.down * speed++ * Time.deltaTime * 0.4f);

        if (mainCam.transform.position.y < -10)
        {
            clearInfoPanel.SetActive(true);
            gameClear = false;
        }
        //seta = Mathf.Deg2Rad * Mathf.Lerp(0.0f, 90f * Mathf.Deg2Rad, Time.deltaTime);
        //float sinValue = Mathf.Sin(seta);
        //float cosValue = Mathf.Cos(seta);

        //float x = cosValue / 3; // 값이 크게 나옴
        //float y = sinValue / 3;

        //mainCam.transform.Translate(new Vector3(-x, 0, -y));
    }
    public void GameClear(){ gameClear = true; }

    public void LoadStage(){ SceneManager.LoadScene("StageSelect"); }
}
