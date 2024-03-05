using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AirWall : MonoBehaviour
{
    void Death()
    {
        PoolMgr.GetInstance().PushObj(PoolName.AirWall,this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if ( player!= null)
        {
            player.circleCollider.isTrigger =false;
        }
    }

}
