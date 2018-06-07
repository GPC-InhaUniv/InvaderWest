using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttribute : MonoBehaviour {

    public float Hp;
    public float maxHp;
    public bool Dead;

    public GameObject explosion;
    public GameObject playerexplosion;

    public int ScoreValue;

    private GameController gameController;

    private void Start()
    {

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("게임 컨트롤러를 찾을 수 없습니다.");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary") //해당 스크립트를 적용한 것이 바운더리에 충돌한다면
        {
            return;
        }
        if(other.tag == "Enemy")
        {
            return;
        }
        if(other.tag == "Bolt")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Hp -= 10;
            if(Hp == 0)
            {
                gameController.GameClear();
                Instantiate(playerexplosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }             
        }
        
        if (other.tag == "Player") //플레이어가 부딪힌다면
        {
            Instantiate(playerexplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }
        gameController.AddSocre(ScoreValue);
        Destroy(other.gameObject);
    }
}
