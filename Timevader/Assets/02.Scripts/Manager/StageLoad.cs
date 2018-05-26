using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoad : MonoBehaviour {

    public int? StageNumber;
    public StageManager stageManager;

    private void Start()
    {
        stageManager = FindObjectOfType<StageManager>();
    }

    public void LoadStage()
    {
        Debug.Log("stagenumber : " + StageNumber + "   , Nextstage : " + stageManager.NextStage);
        if (StageNumber == stageManager.NextStage)
        {
            SceneManager.LoadScene("Stage" + StageNumber);
        }
        Debug.Log("접근할 수 없는 스테이지");
    }
}
