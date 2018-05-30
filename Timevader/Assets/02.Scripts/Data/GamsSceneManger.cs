using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamsSceneManger : MonoBehaviour {


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void GoLogInScene()
    {
        SceneManager.LoadScene("LogIn");
    }
    public void GoMainScene()
    {
        SceneManager.LoadScene("Main");

    }
    public void GoShipSelectScene()
    {
        SceneManager.LoadScene("ShipSelect");

    }
    public void GoShopScene()
    {

    }
    public void GoStageSelectScene()
    {

    }
    public void GoStoryScene()
    {

    }

}
