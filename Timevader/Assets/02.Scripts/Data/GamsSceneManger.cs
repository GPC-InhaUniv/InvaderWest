using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamsSceneManger : MonoBehaviour {

    //Test 씬 이동 편하게//
    public void GoLogInScene()
    {
        SceneManager.LoadScene("LogIn");
    }
    public void GoMainScene()
    {
        SceneManager.LoadScene("Main");

    }
    public void GoSpaceShipSelectScene()
    {
        SceneManager.LoadScene("SpaceShipSelect");

    }
    public void GoShopScene()
    {
        SceneManager.LoadScene("Shop");

    }
    public void GoStageSelectScene()
    {
        SceneManager.LoadScene("StageSelect");

    }
    public void GoStoryScene()
    {
        SceneManager.LoadScene("Story");

    }
    public void GoStage1Scene()
    {
        SceneManager.LoadScene("Stage1");

    }
    public void GoStage2Scene()
    {
        SceneManager.LoadScene("Stage2");

    }
    public void GoStage3Scene()
    {
        SceneManager.LoadScene("Stage3");

    }

}
