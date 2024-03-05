using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHalo : Monster
{

    public GameObject Bullet;
    public float speedY;
    private int faceDir = 1;
    private float atk_Time = 0;
    private float time = 0;
    private float timeY = 0;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        name = PoolName.GhostHalo;
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_Death)
        {

            //计算与主角的位置
            float x = this.transform.position.x - player.transform.position.x;
            float y = this.transform.position.y - player.transform.position.y;
            //朝相
            if (x > 0)
            {
                faceDir = -1;
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (x < 0)
            {
                faceDir = 1;
                this.GetComponent<SpriteRenderer>().flipX = false;
            }

            //移动相关
            time += Time.deltaTime;
            timeY += Time.deltaTime;
            //x移动
            if (Mathf.Abs(x) > 5)
                rigidbody.velocity = new Vector2(faceDir * speed, rigidbody.velocity.y);
            else
            {
                if (time > 8)
                {
                    int dir = Random.Range(0, 10);
                    if (dir >= 5)
                    {
                        rigidbody.velocity = new Vector2(faceDir * speed, rigidbody.velocity.y);
                    }
                    else
                    {
                        rigidbody.velocity = new Vector2(-faceDir * speed, rigidbody.velocity.y);
                    }
                    time = 0;
                }
            }
            //y移动
            if (timeY > 1)
            {
             
                if (y < 2)
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, speedY);
                else if(y>5)
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, -speedY);

                timeY = 0;
            }
            //攻击
            atk_Time += Time.deltaTime;
            if (atk_Time >= Random.Range(3,5)&&Mathf.Abs(x)<5&&Mathf.Abs(y)<3)
            {
                PoolMgr.GetInstance().GetObj(PoolName.BigBullet,(obj)=> {
                    obj.transform.position = this.transform.position;
                    obj.GetComponent<BigBullet>().atkDir = player.transform.position - this.transform.position;
                });

                

                atk_Time = 0;
            }
            //判断是否撞到了墙
            RaycastHit2D info = Physics2D.Raycast(this.transform.position, Vector2.right * -faceDir, 0.2f, 1 << LayerMask.NameToLayer("AirWall"));
            if (info.transform != null)
            {
                rigidbody.velocity = new Vector2(faceDir * speed, rigidbody.velocity.y);
            }
        }
    }
    public override void Hurt(int value)
    {
        base.Hurt(value);
    }
    public override void Death()
    {
        base.Death();
        rigidbody.velocity = Vector3.zero;

    }
}
