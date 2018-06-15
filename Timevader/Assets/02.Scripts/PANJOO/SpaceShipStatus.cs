using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipStatus : MonoBehaviour {
    [SerializeField]
    float speed;//set하면 넘겨주기 !!
    public float Speed { get { return speed; } }

    [SerializeField]
    int life;
    public int Life { get { return life; } }

    [SerializeField]
    string spaceShipName;
    public string SpaceShipName { get { return spaceShipName; } }

    [SerializeField]
    int spaceShipPrice;
    public int SpaceShipPrice { get { return spaceShipPrice; } }
}
