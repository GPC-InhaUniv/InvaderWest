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
    private int? playerRestTime = null;

    [SerializeField]
    private int playerLife;

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
    //플레이어 시간//
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
    public void DisplayPlayerRestTime()
    {
        RestTimeScoreText.text = playerRestTime.ToString();
        Debug.Log(playerRestTime);
    }

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
