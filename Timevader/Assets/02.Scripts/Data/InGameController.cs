using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 

public class InGameController : MonoBehaviour
{
    public GameObject GameWinResultPanel;
    public GameObject GameLoseResultPanel;

    public Text RestTimeScoreText;
    public Image[] LifeImage;

    [SerializeField]
    private int playerRestTime;
    [SerializeField]
    private int playerLife;

    //플레이어 남은 라이프 업데이트//
    public void UpdatePlayerLife(int playerLife)
    {
        Debug.Log(playerLife);
        this.playerLife = playerLife;

        if (this.playerLife>0)
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
        Debug.Log(playerRestTime);

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
    public void DisplayPlayerRestTime()
    {
        RestTimeScoreText.text = playerRestTime.ToString();
        Debug.Log(playerRestTime);
    }

    //남은 라이프 보여주기//
    public void DisPlayPlayerLifeImage(int life)
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


}
