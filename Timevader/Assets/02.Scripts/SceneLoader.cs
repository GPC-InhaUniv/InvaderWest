using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    [SerializeField]
    private GameObject Earth;

    [SerializeField]
    private float ConditionRotation;


	void Start () {
        ConditionRotation = 30.0f;
    }
	
	// Update is called once per frame
	void Update () {
        
        LoadNextScene();
        
}
    public void LoadNextScene()
    {
        while(Earth.transform.rotation.z > ConditionRotation)
        {
            Debug.Log("다음 씬으로");
            
        }
        SceneManager.LoadScene("Main");
    }
}
