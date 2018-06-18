using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {

    BossAttribute attri;

    [SerializeField]
    private Slider hpBar;

	void Start ()
    {
       attri = gameObject.GetComponent<BossAttribute>();
	}

	void Update ()
    {
       hpBar.value = attri.BossHp / attri.MaxHp;
    }
}
