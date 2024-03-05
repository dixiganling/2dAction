using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtk : MonoBehaviour
{
    public Player player;
    public EdgeCollider2D edgeCollider;
    private float atkItemsBack = 2;
    public float atkItemsUp = 1;
    public float playerSpeedInfectBack = 1;
    private string name;
    private void Start()
    {
        name = gameObject.name;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Monster")
        {
            if (name == "Atk1")
                MusicMgr.GetInstance().PlaySound("Player/Atk/Atk1");
            if (name == "Atk2")
                MusicMgr.GetInstance().PlaySound("Player/Atk/Atk2");
            if (name == "Atk3")
                MusicMgr.GetInstance().PlaySound("Player/Atk/Atk3");
            //怪物受伤
            collision.GetComponent<Monster>().Hurt(player.playerModel.atk);
            if (!collision.name.Contains("Ghost"))
            {
                //获取人物与物品位置向量
                Vector3 v = collision.transform.position - player.transform.position;
                //冻结z轴
                v.z = 0;
                v.y = Random.RandomRange(0.6f,1.4f);
                float h = player.InputX;
                if (name == "Atk3")
                    collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                else
                    collision.GetComponent<Rigidbody2D>().velocity = v * atkItemsBack + Vector3.right * h / 2f * playerSpeedInfectBack * 5;


            }
           

        }
    }

}