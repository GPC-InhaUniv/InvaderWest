using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoad : MonoBehaviour {

    public int? StageNumber;
    public TEmp_StageManager tempStageManager;

    private void Start()
    {
        tempStageManager = FindObjectOfType<TEmp_StageManager>();
    }

    public void LoadStage()
    {
        Debug.Log("stagenumber : " + StageNumber + "   , Nextstage : " + tempStageManager.NextStage);
        if (StageNumber == tempStageManager.NextStage)
        {
            SceneManager.LoadScene("Stage" + StageNumber);
        }
        Debug.Log("접근할 수 없는 스테이지");
    }
}
