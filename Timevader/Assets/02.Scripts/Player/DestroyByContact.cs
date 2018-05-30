using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistroyByContact : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
 