using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temp_StageClear : MonoBehaviour {
    [SerializeField]
    private GameObject camRotater, clearInfoPanel, player;

    const float ZOONVALUE = 30.0f, INITVALUE = 60.0f;
    float t = 0f;

    enum GameState
    {
        GAMECLEAR = 1 << 0,
        CAMERAZOOM = 1 << 1,
    }

    GameState gameState;
    Vector3 camPos;
    int moveSpeed = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if ((gameState & GameState.GAMECLEAR) == GameState.GAMECLEAR) SetCamera();
        if ((gameState & GameState.CAMERAZOOM) == GameState.CAMERAZOOM) MoveCamera();
    }

    public void SetCamera()
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

    public void MoveCamera()
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
