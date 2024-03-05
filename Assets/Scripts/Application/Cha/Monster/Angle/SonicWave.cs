using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicWave : ReusableObject
{
    private float Add;
    public int Atk;
    private void Update()
    {
        //不断变大
        Add += Time.deltaTime;
        this.transform.localScale = Vector3.one + Vector3.one * Add;
        //回收
        if (this.transform.localScale.x > 8)
        {
            PoolMgr.GetInstance().PushObj(PoolName.SonicWave, this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player != null)
        {
            if (!player.is_Roll)
            {
                collision.GetComponent<Player>().Hurt(Atk);
                PoolMgr.GetInstance().PushObj(PoolName.SonicWave, this.gameObject);
            }
        }
     
            
    }

    public override void OnSpawn()
    {
        
    }

    public override void OnUnSpawn()
    {
        this.transform.localScale = Vector3.one;
        Add = 0;
    }
}
