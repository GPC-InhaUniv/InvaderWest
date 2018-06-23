using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temp_StageClear : MonoBehaviour {
    [SerializeField]
    GameObject camRotater, clearInfoPanel, player, boss;

    [SerializeField]
    float ZoomSpeed = 1.5f;

    const float ZOONVALUE = 40.0f, INITVALUE = 60.0f;
    float t = 0f;
    bool bossZoomComplete = false; // 임시로 사용

    GameState nowGameState;
    Vector3 camPos; // 카메라가 위치할 좌표. Zoom에 사용
    float moveSpeed = 0f;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");
        //camPos = new Vector3(boss.transform.position.x, boss.transform.position.y, Camera.main.transform.position.z);
        //Debug.Log(camPos);
    }

    void FixedUpdate()
    {
        if (nowGameState == GameState.Ready)
        {
            camPos = new Vector3(boss.transform.position.x, boss.transform.position.y - 1, Camera.main.transform.position.z);
            ZoomCamera();
        }
        else if(nowGameState == GameState.CameraEffect)
        {
            ZoomOutCamera();
        }
        else if (nowGameState == GameState.Win)
        {
            camPos = new Vector3(player.transform.position.x, player.transform.position.y + 1, Camera.main.transform.position.z);
            ZoomCamera();
        }
        else if (nowGameState == GameState.WinEvent) MoveCamera();

        //if ((gameState & GameStage2.BOSSAPPEAR) == GameStage2.GAMECLEAR);
        //else if ((gameState & GameStage2.GAMECLEAR) == GameStage2.GAMECLEAR) SetCamera();
        //else if ((gameState & GameStage2.CAMERAZOOM) == GameStage2.CAMERAZOOM) MoveCamera();
    }

    public void ZoomCamera() // 카메라 줌
    {
        //Camera.main.transform.position = Vector3.Lerp(camPos, new Vector3(0.0f, 4.0f, -10.0f), Time.deltaTime * 3.0f); // 기존 2.5
        //Camera.main.transform.position = Vector3.Lerp(camPos, new Vector3(0.0f, 4.0f, -10.0f), Time.deltaTime * 2.5f); // 기존 2.5
        //Camera.main.transform.position = new Vector3(boss.transform.position.x, boss.transform.position.y - 2, Camera.main.transform.position.z);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, camPos, Time.deltaTime * 2.5f); // 기존 2.5
        Camera.main.fieldOfView = Mathf.Lerp(INITVALUE, ZOONVALUE, t);
        t += ZoomSpeed * Time.deltaTime;

        if (Camera.main.fieldOfView <= ZOONVALUE) // 줌인 완료
        {
            //gameState |= GameStage2.CAMERAZOOM;
            //gameState = gameState & ~GameStage2.GAMECLEAR;

            //GameState.Started;
            GamePlayManager.Instance.NowGameState = GameState.CameraEffect;
            Debug.Log(bossZoomComplete);
        }
    }

    public void ZoomOutCamera()
    {
        Camera.main.fieldOfView = Mathf.Lerp(ZOONVALUE, INITVALUE, t);
        t += ZoomSpeed * Time.deltaTime;

        if (Camera.main.fieldOfView >= INITVALUE) // 줌인 완료
        {
            // 초기 카메라 위치로 리셋
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(0.0f, 0.0f, -10.0f), Time.deltaTime * 3.0f); // 기존 2.5
            //t = 0f;
        }
        GamePlayManager.Instance.NowGameState = GameState.Started;


    }

    public void MoveCamera() // Clear 시 카메라 회전 및 이동(플레이어가 이동하는 듯한 연출)
    {
        camRotater.transform.Rotate(0, Mathf.Lerp(0f, -90f, Time.deltaTime * 1.2f), 0);
        Camera.main.fieldOfView = Mathf.Lerp(ZOONVALUE, INITVALUE, t);
        Camera.main.transform.Translate(Vector3.down * moveSpeed++ * Time.deltaTime * 0.4f);
        t += ZoomSpeed * Time.deltaTime;

        if (Camera.main.transform.position.y <= -12.0f) // 이동 연출 완료
        {
            //clearInfoPanel.SetActive(true);
            //gameState = gameState & ~GameStage2.CAMERAZOOM;
        }
    }

    //public void GameClear()
    //{
    //    //gameState |= GameStage2.GAMECLEAR;
    //    camPos = new Vector3(player.transform.position.x, player.transform.position.y + 1, Camera.main.transform.position.z);
    //}
    //public void LoadStage(){ SceneManager.LoadScene("StageSelect"); }

    IEnumerator checkGameState()
    {
        nowGameState = GamePlayManager.Instance.NowGameState;
        Debug.Log(nowGameState);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("checkGameState");
    }
}
