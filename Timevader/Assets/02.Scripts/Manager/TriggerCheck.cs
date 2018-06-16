using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerCheck : MonoBehaviour {
    public Text StageLv, StageInfo;
    StageInfoData info;
    public StageLoad stageLoad;

    private void Start()
    {
        StageLv.text = string.Empty;
        StageInfo.text = string.Empty;
        stageLoad = GameObject.FindGameObjectWithTag("GameController").GetComponent<StageLoad>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if (other.tag == "StageSelect")
        {
            info = other.gameObject.GetComponent<StageInfoData>(); // ※ 실행 중에 GetComponent ※
            other.gameObject.GetComponent<Animator>().enabled = true; // ※ 실행 중에 GetComponent ※
            other.gameObject.GetComponent<Animator>().SetTrigger("Checked"); // ※ 실행 중에 GetComponent ※

            int StageNumber;
            int.TryParse(info.GetStageLv(), out StageNumber);

            stageLoad.StageNumber = StageNumber;
            StageLv.text = "Stage" + info.GetStageLv();
            StageInfo.text = info.GetStageInfo();
        }

        //int stageNumber;
        //if (!int.TryParse(info.GetStageLv(), out stageNumber))
        //    Debug.Log("Stage Lv을 받아오는데 실패");
        //stageLoad.StageNumber = stageNumber;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
        other.gameObject.GetComponent<Animator>().enabled = false;
        StageLv.text = string.Empty;
        StageInfo.text = string.Empty;
        stageLoad.StageNumber = null;
    }
}
