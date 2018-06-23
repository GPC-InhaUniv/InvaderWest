using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Line_LeftToRight,
    Line_RightToLeft,
    Circle_Clockwise, // 특정 좌표를 기준으로 회전
    Circle_CounterClockwise,
    Zigzag_LeftToRight, // 지그재그
    Zigzag_RightToLeft,
    Curve_RightDown, // 곡선,
    Curve_LeftDown,
}

public enum InvaderType
{
    Normal,
    Wrecked,
    Attacking,
}

/*  FlyWeghit Pattern
 *  GameManager에 배치할 것 */
public class Enemy : MonoBehaviour
{

    public GameObject[] Items;
    [SerializeField]
    public AudioSource destroyAudio;

    [SerializeField] [Range(0, 100)]
    int ItemDropProbability; // 20%

    [SerializeField] [Range(0, 100)]
    int WreckProbability; // 50%

    [SerializeField]
    protected float moveSpeed, moveHeight, circleSpeed;
    protected float radius;
    
    protected Enemy enemy;
    protected const int MAXSETA = 360;
    protected string[] itemList = { ((ItemList)1).ToString(), ((ItemList)2).ToString() };

    void Awake()
    {
        moveSpeed = 5.0f;
        moveHeight = 6.0f; // Zigzag, Curve에서 사용하는 높이 변화량
        radius = 5.0f; // Circle
        circleSpeed = 4.0f;
        enemy = GameObject.FindWithTag("Factory").GetComponent<Enemy>();
        //Enemy전체 Explode
        PlayerShip.OnUseItem += Explode; 
    }

    virtual protected void Move() { }
    virtual protected void MoveCircle(bool sign) { }
    virtual protected void MoveZigzag(bool sign) { }
    virtual protected void MoveCurve(bool sign) { }
    virtual protected void Init() { }
    virtual protected void GetDemage(int damage) { }

    protected void Explode()
    {
        //Explode될때 소리 추가//
        destroyAudio.Play();
        GameObject explosion = PoolController.instance.GetFromPool(PoolType.ExplosionPool);
        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); // 이펙트 크기를 2배로
        }

        if (Random.Range(1, 100) <= WreckProbability)
            Wrecked();

        if (Random.Range(1, 100) <= ItemDropProbability)
            DropItem(ItemList.AddMissileItem);
        ReturnToPool();
    }

    void DropItem(ItemList num)
    {
        Debug.Log((int)num);
        GameObject item = PoolController.instance.GetFromPool(PoolType.ItemPool);
        item.transform.position = transform.position;
        item.transform.rotation = Quaternion.identity;
        //Instantiate(Items[0], transform.position, Quaternion.identity);
    }
    /* 우주선이 파괴되면 30% 확률로 잔해가 되어 아래로 점점 떨어진다. 
     * 플레이어에 닿으면 데미지를 준다. 미사일, 플레이어와 충돌 시 파괴 */

    void Wrecked() 
    {
        Debug.Log("WRECKED");
        GameObject wreckedShip = PoolController.instance.GetFromPool(PoolType.WreckedPool);
        wreckedShip.transform.position = transform.position;
        //Instantiate(WreckedShip, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag == "Player" || other.gameObject.tag == "Bolt")
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bolt"))
        {
            GetDemage(2); // other.power
        }
    }

    void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.tag == "BackGround") ReturnToPool();
        if(other.gameObject.CompareTag("BackGround")) ReturnToPool();
    }

    void ReturnToPool()
    {
        // 오브젝트가 화면 밖으로 빠져나가면 false로 변경
        Init();
        this.gameObject.SetActive(false);
    }

    virtual public void SetDirection(Direction dir) { }
}
