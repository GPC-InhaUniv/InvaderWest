using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyModel : MonoBehaviour {

    public Sprite WreckedImg;
    Image img;

    private void Start()
    {
        img = GetComponent<Image>();
    }

    public void Move() // 대각선 이동 // 원형 회전 // 지그재그 이동 // 직선 이동
    {

    }

    public void Attack()
    {

    }

    public void Wrecked()
    {
        /* 우주선이 파괴되면 잔해가 되어 아래로 점점 떨어진다. 
         * 플레이어나 우주선에 닿으면 데미지를 준다. 파괴되지 않는다. */
        img.sprite = WreckedImg;
    }

    public void DropItem()
    {

    }
}
