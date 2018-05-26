using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//상점 실험 포션//
[System.Serializable]
public class StoreItem
{
    [SerializeField]
    private string name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    [SerializeField]
    private int cost;
    public int Cost
    {
        get { return cost; }
        set { cost = value; }
    }
}
