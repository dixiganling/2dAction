using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordQi : MonoBehaviour
{
    [NonSerialized]
    public Vector2 Size;
   private void Explode()
    {

        //生成判定
        Collider2D collider2D = Physics2D.OverlapBox(this.transform.position, Size, 0, 1 << LayerMask.NameToLayer("Player"));
        //让角色受伤
        if (collider2D != null)
            collider2D.GetComponent<Player>().Hurt(8);
    }

    //回收
    private void Dead()
    {
        PoolMgr.GetInstance().PushObj(PoolName.SwordQi, this.gameObject);
        
    }
    //void OnDrawGizmos()
    //{
    //    Vector3 center = this.transform.position;


    //    //绘制框形状
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(center, Size);
    //}
}
