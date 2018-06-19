using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temp_StageClear : MonoBehaviour {
    [SerializeField]
    private GameObject camRotater, clearInfoPanel, player, boss;

    const float ZOONVALUE = 30.0f, INITVALUE = 60.0f;
    float t = 0f;

    enum GameState
    {
        BOSSAPPEAR = 1 << 0,
        GAMECLEAR = 1 << 1,
        CAMERAZOOM = 1 << 2,
    }

    GameState gameState;
    Vector3 camPos; // 카메라가 위치할 좌표. Zoom에 사용
    float moveSpeed = 0f
        ;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    private void Update()
    {
        if ((gameState & GameState.BOSSAPPEAR) == GameState.GAMECLEAR) ;
        else if ((gameState & GameState.GAMECLEAR) == GameState.GAMECLEAR) SetCamera();
        else if ((gameState & GameState.CAMERAZOOM) == GameState.CAMERAZOOM) MoveCamera();
    }

    public void SetCamera() // 카메라 줌
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, camPos, Time.deltaTime * 2.5f);
        Camera.main.fieldOfView = Mathf.Lerp(INITVALUE, ZOONVALUE, t);
        t += 0.9f * Time.deltaTime;

        if (Camera.main.fieldOfView <= ZOONVALUE) // 줌인 완료
        {
            gameState |= GameState.CAMERAZOOM;
            gameState = gameState & ~GameState.GAMECLEAR;
            t = 0f;
        }
    }

    public void MoveCamera() // Clear 시 카메라 회전 및 이동(플레이어가 이동하는 듯한 연출)
    {
        camRotater.transform.Rotate(0, Mathf.Lerp(0f, -90f, Time.deltaTime * 1.2f), 0);
        Camera.main.fieldOfView = Mathf.Lerp(ZOONVALUE, INITVALUE, t);
        Camera.main.transform.Translate(Vector3.down * moveSpeed++ * Time.deltaTime * 0.4f);
        t += 1.5f * Time.deltaTime;

        if (Camera.main.transform.position.y <= -12.0f) // 이동 연출 완료
        {
            clearInfoPanel.SetActive(true);
            gameState = gameState & ~GameState.CAMERAZOOM;
        }
    }

    public void GameClear()
    {
        gameState |= GameState.GAMECLEAR;
        camPos = new Vector3(player.transform.position.x, player.transform.position.y + 1, Camera.main.transform.position.z);
    }
    public void LoadStage(){ SceneManager.LoadScene("StageSelect"); }
}
