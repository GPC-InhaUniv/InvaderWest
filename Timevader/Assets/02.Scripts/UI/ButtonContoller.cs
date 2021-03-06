﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonContoller : MonoBehaviour {

	public void ChangeSceneByShopPCChoice()
    {
        SceneManager.LoadScene("SelectSpaceShip");
    }
    public void ChangeSceneByShop()
    {
        SceneManager.LoadScene("Shop");
    }
    public void ChangeSceneByStageSelect()
    {
        SceneManager.LoadScene("SelectStage");
    }
    public void ChangeSceneByMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void ChangeSceneByIntro()
    {
        SceneManager.LoadScene("Intro");
    }
    public void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
