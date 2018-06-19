using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoad : MonoBehaviour {

    [SerializeField]
    int? stageNumber;
    public int? StageNumber { get { return stageNumber; } set { stageNumber = value; } }
    StageController stageController;

    void Start()
    {
        stageController = FindObjectOfType<StageController>();
    }

    public void LoadStage()
    {
        Debug.Log("stagenumber : " + StageNumber + "   , Nextstage : " + stageController.GetNextStageInfo());
        if (StageNumber == stageController.GetNextStageInfo())
        {
            // SceneManager.LoadScene("Stage" + StageNumber);
            SceneManager.LoadScene("Test_Stage3");
        }
        Debug.Log("접근할 수 없는 스테이지");
    }

    

    //public void SetStageNumber(int? num)
    //{
    //    StageNumber = num;
    //}
}
