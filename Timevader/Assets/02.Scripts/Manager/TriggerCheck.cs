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
        StageInfo.text = string.Empty;
        stageLoad = GameObject.FindGameObjectWithTag("GameController").GetComponent<StageLoad>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        info = other.gameObject.GetComponent<StageInfoData>(); // ※ 실행 중에 GetComponent ※
        other.gameObject.GetComponent<Animator>().SetTrigger("Checked"); // ※ 실행 중에 GetComponent ※

        stageLoad.StageNumber = int.Parse(info.GetStageLv());
        StageLv.text = "Stage" + info.GetStageLv();
        StageInfo.text = info.GetStageInfo();

        //if (!int.TryParse(info.GetStageLv(), out stageLoad.StageNumber))
        //    Debug.Log("Stage Lv을 받아오는데 실패");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
        other.gameObject.GetComponent<Animator>().SetTrigger("Normal"); // ※ 실행 중에 GetComponent ※
        StageInfo.text = string.Empty;
        stageLoad.StageNumber = null;
    }
}
