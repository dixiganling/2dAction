using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Monster
{
    public GameObject Bullet;
    private float atk_Time = 0;
    //public Vector2 Size;
    private int faceDir = 1;
    //public float f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        name = PoolName.Wizard;
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_Death)
        {
            //计算与主角的位置
            float x = this.transform.position.x - player.transform.position.x;
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
            if (!isHurt)
            {
                //移动
                if (Mathf.Abs(x) < 5)
                {
                    rigidbody.velocity = new Vector2(-faceDir * speed, rigidbody.velocity.y);
                }
                if (Mathf.Abs(x) > 8)
                {
                    rigidbody.velocity = new Vector2(faceDir * speed, rigidbody.velocity.y);
                }
                //攻击事件判定
                atk_Time += Time.deltaTime;
                //攻击
                if (Mathf.Abs(x) < 7 && atk_Time > Random.Range(5f, 10f))
                {
                    MusicMgr.GetInstance().PlaySound("FireBall");
                    animator.SetTrigger("Attack");
                    atk_Time = 0;
                }
            }
        
          
            //判断是否撞到了墙
            RaycastHit2D info = Physics2D.Raycast(this.transform.position, Vector2.right * -faceDir, 0.2f, 1 << LayerMask.NameToLayer("AirWall"));
            if (info.transform != null)
            {
                rigidbody.velocity = new Vector2(faceDir * speed, rigidbody.velocity.y);
            }
            //Debug.DrawRay(this.transform.position,Vector2.down *f);
           /* RaycastHit2D ray = Physics2D.Raycast(this.transform.position, Vector2.down , 1, 1 << LayerMask.NameToLayer("Ground"));
            if(ray.transform != null)
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

    //void OnDrawGizmos()
    //{
    //    Vector3 center = this.transform.position;


    //    //绘制框形状
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(center, Size);
    //}

    private void AtkEvent()
    {
        //产生火球
        PoolMgr.GetInstance().GetObj(PoolName.FollowBall,(obj)=> {
            obj.transform.position = this.transform.position+new Vector3(0,0.3f);
        });
    }
}
