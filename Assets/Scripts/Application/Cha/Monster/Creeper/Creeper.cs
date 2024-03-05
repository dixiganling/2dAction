using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creeper : Monster
{
    //public Vector2 Size;
    private int faceDir = 1;
    public int boomAtk;
    float x;
    public float boomTime;
    //是否开启了自爆
    private bool isBoom;
    protected override void Start()
    {
        base.Start();
        name = PoolName.Creeper;
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_Death)
        {
            
            //计算与主角的位置
            x = this.transform.position.x - player.transform.position.x;
            //朝相
            if (x > 0)
            {
                faceDir = -1;
                this.GetComponent<SpriteRenderer>().flipX = false;
            }

            else if (x < 0)
            {
                faceDir = 1;
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            if(!isHurt)
            this.rigidbody.velocity = new Vector2(speed * faceDir, this.rigidbody.velocity.y);
            //爆炸时间
            if (isBoom)
                boomTime += Time.deltaTime;
            //自爆距离
            if (Mathf.Abs(x) <=3.5f&&!isBoom)
            {
                isBoom = true;
                //进入自爆状态
                animator.SetBool("Explode",isBoom);            
            }
            //时间到了就自爆
            if(boomTime >= 3)
            {
                Explode();
                boomTime = 0;
            }
            //判断是否撞到了墙
            RaycastHit2D info = Physics2D.Raycast(this.transform.position, Vector2.right * -faceDir, 0.2f, 1 << LayerMask.NameToLayer("AirWall"));
            if (info.transform != null)
            {
                rigidbody.velocity = new Vector2(faceDir * speed, rigidbody.velocity.y);
            }
            //Debug.DrawRay(this.transform.position,Vector2.down *f);
           /* RaycastHit2D ray = Physics2D.Raycast(this.transform.position, Vector2.down, 1.2f, 1 << LayerMask.NameToLayer("Ground"));
            if (ray.transform != null)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
                rigidbody.gravityScale = 0;
            }
            else
            {
                rigidbody.gravityScale = 1;
            }*/
        }
    }
    private void Explode()
    {
        animator.SetTrigger("Boom");
    }
    private void Boom()
    {
 
        //判定
        Collider2D collider2D = Physics2D.OverlapBox(this.transform.position, new Vector2(2,2), 0,1<< LayerMask.NameToLayer("Player"));
        player.rigidbody.velocity = Vector2.zero;
        if (collider2D!= null)
        {
            if (x < 0)
                player.rigidbody.AddRelativeForce(new Vector2(200, 200));
            else
                player.rigidbody.AddRelativeForce(new Vector2(-200, 200));
       
            //爆炸
            player.Hurt(boomAtk);
            //使角色中毒
            player.Poisoning();
        }
  
    }
    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
        boomTime = 0;
        isBoom = false;
        animator.SetBool("Explode", isBoom);
    }
    //void OnDrawGizmos()
    //{
    //    Vector3 center = this.transform.position;


    //    //绘制框形状
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(center, Size);
    //}
}
