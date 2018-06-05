using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonContoller : MonoBehaviour {

	public void ChangeSceneByShopPCChoice()
    {
        SceneManager.LoadScene("SpaceShipSelect");
    }
    public void ChangeSceneByShop()
    {
        SceneManager.LoadScene("Shop");
    }
    public void ChangeSceneByStageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }
    public void ChangeSceneByMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void ChangeSceneByIntro()
    {
        SceneManager.LoadScene("Intro");
    }
}
