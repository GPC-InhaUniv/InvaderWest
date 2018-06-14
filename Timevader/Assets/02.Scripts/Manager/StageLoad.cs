using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoad : MonoBehaviour {

    [SerializeField]
    int? StageNumber;
    StageController stageController;

    private void Start()
    {
        stageController = FindObjectOfType<StageController>();
    }

    public void LoadStage()
    {
        Debug.Log("stagenumber : " + StageNumber + "   , Nextstage : " + stageController.GetNextStageInfo());
        if (StageNumber == stageController.GetNextStageInfo())
        {
            SceneManager.LoadScene("Stage" + StageNumber);
        }
        Debug.Log("접근할 수 없는 스테이지");
    }

    public void SetStageNumber(int? num)
    {
        StageNumber = num;
    }
}
