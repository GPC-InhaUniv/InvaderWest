using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temp_StageClear : MonoBehaviour {

    public void LoadStage()
    {
        SceneManager.LoadScene("StageSelect");
    }
}
