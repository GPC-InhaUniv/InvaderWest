using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class InGameController : MonoBehaviour
{
    //플레이어 부분//
    [SerializeField]
    GameObject normalShip;
    [SerializeField]
    GameObject raptorShip;
    [SerializeField]
    GameObject blackHawkShip;

    //UI 부분//
    [SerializeField]
    GameObject gameWinResultPanel;
    [SerializeField]
    GameObject gameLoseResultPanel;
    [SerializeField]
    Text restTimeScoreText;
    [SerializeField]
    Image[] lifeImage;
    [SerializeField]
    Slider bosshpBar;

    [SerializeField]
    int playerRestTime;
    [SerializeField]
    int playerLife;
    [SerializeField]
    int stageData;
    [SerializeField]
    float bossLife;
    [SerializeField]
    float maxBossLife;

    void Awake()
    {
        Vector3 startPosition = new Vector3(0.0f, -4.0f, 0.0f);
        Vector3 startRotation = new Vector3(-90f, 0.0f, 0.0f);
        if (GamePlayManager.Instance.PlayerShipNum == 1)
            Instantiate(normalShip, startPosition, Quaternion.Euler(startRotation));
        else if (GamePlayManager.Instance.PlayerShipNum == 2)
            Instantiate(blackHawkShip, startPosition, Quaternion.Euler(startRotation));
        else if (GamePlayManager.Instance.PlayerShipNum == 3)
            Instantiate(raptorShip, startPosition, Quaternion.Euler(startRotation));
    }
    void Start()
    {

        StartCoroutine(IncreaseHpBar());

        stageData = int.Parse(AccountInfo.Instance.StageData);
    }
    //보스 라이프 업데이트//
    public void UpdateBossLife(float bossLife, float maxBossLife)
    {
        this.bossLife = bossLife;
        if (this.bossLife > 0)
        {
            bosshpBar.value = bossLife / maxBossLife;
            //Debug.Log("BossHp / MaxHp  " + bossLife / maxBossLife);
        }
        else
        {
            bosshpBar.value = bossLife / maxBossLife;
            AccountInfo.ChangeRestTimeData(playerRestTime);
            StartCoroutine("WinResult");
        }               
    }

    IEnumerator IncreaseHpBar()
    {
        float hpValue = 0.03f;
        if(bosshpBar.value < 1.0f)
        {
            bosshpBar.value += hpValue;
        }

        yield return new WaitForSeconds(0.05f);
        StartCoroutine(IncreaseHpBar());
    }

    IEnumerator WinResult()
    {
        //다음 스테이지값 저장//
        int nextStageNum = 1;

        if (stageData <= 2)
        {
            AccountInfo.ChangeStageData(stageData + nextStageNum);
            GamePlayManager.Instance.stageData = stageData + nextStageNum;
            Debug.Log(stageData + nextStageNum);
        }
        else
        {
            AccountInfo.ChangeStageData(nextStageNum);
            GamePlayManager.Instance.stageData = nextStageNum;

            Debug.Log(nextStageNum);
        }
        yield return new WaitForSeconds(1.0f);
        gameWinResultPanel.gameObject.SetActive(true);
    }
    //플레이어 남은 라이프 업데이트//
    public void UpdatePlayerLife(int playerLife)
    {
        Debug.Log(playerLife);
        this.playerLife = playerLife;

        if (this.playerLife > 0)
        {
            Debug.Log("Observer Success  " + playerLife);
            DisPlayPlayerLifeImage(playerLife);
        }
        else
        {
            DisPlayPlayerLifeImage(playerLife);
            gameLoseResultPanel.gameObject.SetActive(true);
            Debug.Log("UpdatePlayerLife");
        }
    }

    //플레이어 남은 시간 업데이트//
    public void UpdatePlayerRestTime(int playerRestTime)
    {
        //Debug.Log(playerRestTime);

        this.playerRestTime = playerRestTime;

        if (playerRestTime > 0)
        {
            this.playerRestTime = playerRestTime;
            DisplayPlayerRestTime();
        }
        else
        {
            gameLoseResultPanel.gameObject.SetActive(true);
            Debug.Log("UpdatePlayerRestTime");
        }
    }
    //남은시간 보여주기//
    void DisplayPlayerRestTime()
    {
        restTimeScoreText.text = playerRestTime.ToString();
        //Debug.Log(playerRestTime);
    }

    //남은 라이프 보여주기//
    void DisPlayPlayerLifeImage(int life)
    {
        for (int i = 0; life < lifeImage.Length; i++)
        {
            if (lifeImage[i].gameObject.activeSelf == true)
            {
                lifeImage[i].gameObject.SetActive(false);
                return;
            }
        }
    }
    public void OnGoToNextStage()
    {
        if (stageData == 1)
            SceneManager.LoadScene("Stage2");
        else if (stageData == 2)
            SceneManager.LoadScene("Test_Stage3");
        else
            SceneManager.LoadScene("Main");
    }
    public void OnBackToMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void OnGoToShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void OnPaused()
    {
        Time.timeScale = 0;
        Debug.Log(Time.timeScale);
    }
    public void OnResumed()
    {
        Time.timeScale = 1;
        Debug.Log(Time.timeScale);
    }
}
