using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : ReusableObject
{
    protected int hp;
    public int id;
    public int maxHp;
    public float speed;
    protected bool is_Death;
    protected string name;
    public bool isHurt;
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = Mathf.Clamp(value, 0, maxHp);
            if (hp <= 0)
            {
                Death();
            }
        }
    }
    protected Animator animator;
    protected Rigidbody2D rigidbody;
    protected Player player;
    CreateMonstersProxy proxy;
    protected virtual void Start()
    {
        is_Death = false;
        HP = maxHp;
        animator = this.GetComponent<Animator>();
        rigidbody = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        //被攻击判定

            if (collision is EdgeCollider2D)
            {        
                Hurt(player.playerModel.atk);
            }        
      
    }*/
    public virtual void Hurt(int value)
    {
        if (!is_Death)
            animator.SetTrigger("Hurt");
        isHurt = true;
        HP -= value;
        if (player.playerModel.xiXue)
        {
            player.Hp += player.playerModel.xiXueValue;
        }
    }

    private void HurtEnd()
    {
        isHurt = false;
    }
    public virtual void Death()
    {
        is_Death = true;
        animator.SetBool("Death", is_Death);
    }
    /// <summary>
    /// 死亡时调用
    /// </summary>
    void Recycle()
    {
        PoolMgr.GetInstance().PushObj(name, this.gameObject);
        if (id != -1)
        {
             proxy = GameFacade.GetInstance().RetrieveProxy(CreateMonstersProxy.NAME) as CreateMonstersProxy;

            proxy.ReduceMonster(id);
        }
        //boss
        else
        {
            //去掉空气墙
            GameFacade.GetInstance().SendNotification(CommandName.DELETE_AIRWALLS);
        }
      
    }

    public override void OnSpawn()
    {
        HP = maxHp;

    }

    public override void OnUnSpawn()
    {
        is_Death = false;
        animator.SetBool("Death",is_Death);
        isHurt = false;
    }

}
