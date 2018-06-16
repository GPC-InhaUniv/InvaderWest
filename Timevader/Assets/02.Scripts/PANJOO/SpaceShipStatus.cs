using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipStatus : MonoBehaviour
{
    public ShipStatus staus;
}

[System.Serializable]
public struct ShipStatus
{
    [SerializeField]
    float speed;
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