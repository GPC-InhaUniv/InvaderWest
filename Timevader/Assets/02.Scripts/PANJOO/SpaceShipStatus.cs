using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipStatus : MonoBehaviour {
    [SerializeField]
    private float speed;//set하면 넘겨주기 !!
    public float Speed { get { return speed; } }

    [SerializeField]
    private int life;
    public int Life { get { return life; } }

    [SerializeField]
    private string spaceShipName;
    public string SpaceShipName { get { return spaceShipName; } }

    [SerializeField]
    private int spaceShipPrice;
    public int SpaceShipPrice { get { return spaceShipPrice; } }
}
