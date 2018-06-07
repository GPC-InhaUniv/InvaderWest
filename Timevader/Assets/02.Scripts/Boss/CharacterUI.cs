using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {
    private BossAttribute attri;
    public Slider hpBar;

	// Use this for initialization
	void Start () {
        attri = gameObject.GetComponent<BossAttribute>();
	}
	
	// Update is called once per frame
	void Update () {
            hpBar.value = attri.Hp / attri.maxHp;
      	}
}
