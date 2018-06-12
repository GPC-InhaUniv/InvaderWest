using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public enum ItemKind
    {
        AddMissileItem,
        AssistantItem,
        IncreasingShotSpeedItem,
    };

    protected Player player;
    public ItemKind kind;

    float moveSpeed = 1.0f;
    public float RotateValue = 1.0f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        transform.Rotate(0.0f, RotateValue, 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("아이템 효과 적용");            
            player.GetItem(kind);
            Destroy(gameObject);
        }
    }
}
