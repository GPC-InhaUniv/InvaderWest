using System.Collections;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    [SerializeField]
    public delegate void NotifyObserver();
    NotifyObserver notifyGameStart;

    [SerializeField]
    GameState nowGameState;

    [SerializeField]
    float ZoomSpeed = 1.5f;

    [SerializeField]
    GameObject camRotater, player, boss;

    Spawner spawner;
    Vector3 camPos; // 카메라가 위치할 좌표. Zoom에 사용
    Vector3 defalutCamPos = new Vector3(0.0f, 0.0f, -10.0f);

    const float ZOONVALUE = 40.0f, INITVALUE = 60.0f;
    float moveSpeed = 0f, t = 0;

    bool startSpawnCheck = false; // 임시로 사용

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");
        spawner = GameObject.Find("EnemyFactory").GetComponent<Spawner>();
        notifyGameStart = new NotifyObserver(spawner.StartSpawn);
        GamePlayManager.OnChangeGamestate += CheckGameState;
    }

    void FixedUpdate()
    {
        if (nowGameState == GameState.Ready)
        {
            if (Camera.main.fieldOfView <= ZOONVALUE) // 줌인 완료
            {
                t = 0;
                ChangeGameState();
            }
            else
            {
                camPos = new Vector3(0.0f, boss.transform.position.y - 1, Camera.main.transform.position.z);
                ZoomCamera();
                ChaseCameraTarget();
            }
        }
        else if (nowGameState == GameState.CameraEffect)
        {

            // 줌아웃 완료 & 카메라 위치 리셋
            if (Camera.main.transform.position == defalutCamPos && Camera.main.fieldOfView >= INITVALUE)
            {
                ChangeGameState();
                camPos = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, Camera.main.transform.position.z);
                t = 0;

                if (notifyGameStart != null) 
                    notifyGameStart();
            }
            else
            {
                ZoomOutCamera();
                ResetCameraPos();
            }
        }
        else if (nowGameState == GameState.Win)
        {
            if (Camera.main.fieldOfView <= ZOONVALUE) // 줌인 완료
            {
                t = 0;
                ChangeGameState();
            }
            else
            {
                ZoomCamera();
                ChaseCameraTarget();
            }
        }
        else if (nowGameState == GameState.WinCameraEffect)
        {
            ZoomOutCamera();
            if (Camera.main.fieldOfView >= INITVALUE) // 줌아웃 완료
                RotateCamera();
        }
    }

    void ChangeGameState()
    {
        GamePlayManager.Instance.ChangeGameState();
    }

    void ZoomCamera() // 카메라 줌
    {
        Camera.main.fieldOfView = Mathf.Lerp(INITVALUE, ZOONVALUE, t);
        t += ZoomSpeed * Time.deltaTime;
    }

    void ChaseCameraTarget() // target 추적
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, camPos, Time.deltaTime * 2.5f);
    }

    void ZoomOutCamera()
    {
        Camera.main.fieldOfView = Mathf.Lerp(ZOONVALUE, INITVALUE, t);
        t += ZoomSpeed * Time.deltaTime;
    }

    void ResetCameraPos()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, defalutCamPos, t); // 기존 2.5
    }

    void RotateCamera() // Clear 시 카메라 회전 및 이동(플레이어가 이동하는 듯한 연출)
    {
        Camera.main.transform.Translate(Vector3.down * moveSpeed++ * Time.deltaTime * 0.4f);
        if (Camera.main.transform.position.y >= -12.0f) // 이동 연출 완료
        {
            camRotater.transform.Rotate(0, Mathf.Lerp(0f, -90f, Time.deltaTime * 1.2f), 0);
        }
    }

    public void CheckGameState()
    {
        nowGameState = GamePlayManager.Instance.NowGameState;
    }
}
