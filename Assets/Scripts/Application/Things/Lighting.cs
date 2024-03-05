using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : ReusableObject
{
    private Player player;
    private int atk;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        atk = player.playerModel.lightingHurt;
        MusicMgr.GetInstance().PlaySound("Player/Other/Lighting");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            collision.GetComponent<Monster>().Hurt(atk);
        }
    }
    public void Dead()
    {
        PoolMgr.GetInstance().PushObj(PoolName.Lighting, this.gameObject);
    }

    public override void OnSpawn()
    {
        MusicMgr.GetInstance().PlaySound("Player/Other/Lighting");
    }

    public override void OnUnSpawn()
    {

    }
}
