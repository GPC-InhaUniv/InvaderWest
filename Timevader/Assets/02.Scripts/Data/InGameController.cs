using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class InGameController : MonoBehaviour
{
    public GameObject GameWinResultPanel;
    public GameObject GameLoseResultPanel;

    public Text RestTimeScoreText;
    public Image[] LifeImage;

    [SerializeField]
    int playerRestTime;
    [SerializeField]
    int playerLife;
    [SerializeField]
    int stageData;




    //판주//
    [SerializeField]
    Slider bosshpBar;
    [SerializeField]
    float bossLife;
    [SerializeField]
    float maxBossLife;
    //판주//
    void Start()
    {
        bosshpBar.value = 1.0f;
        stageData = int.Parse(AccountInfo.Instance.StageData);
    }

    //보스 라이프 업데이트//
    public void UpdateBossLife(float bossLife, float maxBossLife)
    {
        this.bossLife = bossLife;
        if (this.bossLife > 0)
        {
            bosshpBar.value = bossLife / maxBossLife;
            Debug.Log("BossHp / MaxHp  " + bossLife / maxBossLife);
        }
        else
        {
            bosshpBar.value = bossLife / maxBossLife;
            //AccountInfo.ChangeRestTimeData(playerRestTime);

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
            GameWinResultPanel.gameObject.SetActive(true);


           // StartCoroutine("WinResult");
        }
                
    }
    //IEnumerator WinResult()
    //{            
    //    ////다음 스테이지값 저장//
    //    //int nextStageNum = 1;

    //    //if (stageData <= 2)
    //    //{
    //    //    AccountInfo.ChangeStageData(stageData + nextStageNum);
    //    //    GamePlayManager.Instance.stageData = stageData + nextStageNum;
    //    //    Debug.Log(stageData + nextStageNum);

    //    //}
    //    //else
    //    //{
    //    //    AccountInfo.ChangeStageData(nextStageNum);
    //    //    GamePlayManager.Instance.stageData = nextStageNum;

    //    //    Debug.Log(nextStageNum);

    //    //}
    //    //yield return new WaitForSeconds(1.0f);
    //    //GameWinResultPanel.gameObject.SetActive(true);

    //}
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
            GameLoseResultPanel.gameObject.SetActive(true);
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
            GameLoseResultPanel.gameObject.SetActive(true);
            Debug.Log("UpdatePlayerRestTime");
        }
    }
    //남은시간 보여주기//
    void DisplayPlayerRestTime()
    {
        RestTimeScoreText.text = playerRestTime.ToString();
        //Debug.Log(playerRestTime);
    }

    //남은 라이프 보여주기//
    void DisPlayPlayerLifeImage(int life)
    {
        //Debug.Log("go");
        for (int i = 0; life < LifeImage.Length; i++)
        {
            if (LifeImage[i].gameObject.activeSelf == true)
            {
                LifeImage[i].gameObject.SetActive(false);
                return;
            }
        }
    }


    public void OnGoToNextStage()
    {
        if (stageData == 1)
        {
            SceneManager.LoadScene("Stage2");
        }
        else if (stageData == 2)
        {
            SceneManager.LoadScene("Stage3");
        }
        else
        {
            SceneManager.LoadScene("Main");
        }
    }
    public void OnBackToMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void OnGoToShop()
    {
        SceneManager.LoadScene("Shop");
    }
}
