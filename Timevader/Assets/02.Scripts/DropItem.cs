using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour {
    public enum ItemKind
    {
        AddMissileitem,
        Assistantitem,
    };

    public ItemKind kind;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.GetItem(kind);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

}
