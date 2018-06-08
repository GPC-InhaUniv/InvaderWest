using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainController : MonoBehaviour {

    public Text FuelScoreText;
    public Text RestTimeText;

    //[SerializeField]
    //private int myFuel = int.Parse(AccountInfo.Instance.Fuel);
    //private int resttime = int.Parse(AccountInfo.Instance.Time);



    // Use this for initialization
    void Start () {

        FuelScoreText.text = AccountInfo.Instance.Fuel;
        RestTimeText.text = AccountInfo.Instance.RestTime;


    }

}
