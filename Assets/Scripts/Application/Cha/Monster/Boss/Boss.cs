using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Monster
{
    private int faceDir = 1;
    private float moveRange;
    private float moveTime = 5;
    private float AtkTime;
    public float AtkSpeed = 1f;
    private float SpecialTime;
    private float SpecialSpeed = 4f;
    private Vector3 SpecialVector3;
    public int Atk = 10;
    private int nowFace;
    private bool isHeiShen;
    private bool HeiShening;
    private bool isRotate;
    private bool isWaitDead;
    public bool isStart;
    private int atkDis = 5;
    public new int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = Mathf.Clamp(value, 0, maxHp);
            if (!isHeiShen)
            {
                if (hp <= maxHp / 2)
                {
                    //设置变身动画
                    animator.SetTrigger("HeiShen");
                    isHeiShen = true;
                    HeiShening = true;
                    //音效
                    MusicMgr.GetInstance().PlaySound("Boss/HeiShen");
                    //增强属性
                    atkDis = 4;
                    Atk = 15;
                    speed = 6;
                }
            }
         
            if (hp <= 0)
            {
                isWaitDead = true;
                animator.SetBool("WaitDead",isWaitDead);
                //显示死亡对话
                GameFacade.GetInstance().SendNotification(CommandName.START_WALL,true,type: "dialog");
            }
        }
    }
    public override void Hurt(int value)
    {
        if (!isWaitDead)
            animator.SetTrigger("Hurt");
        HP -= value;
        if (player.playerModel.xiXue)
        {
            player.Hp += player.playerModel.xiXueValue;
        }
    }
    //public override int HP {

    //    get
    //    {
    //        return hp;
    //    }
    //    set
    //    {
    //        hp = Mathf.Clamp(value, 0, maxHp);
    //        if(hp<= maxHp / 2)
    //        {

    //        }
    //        if (hp <= 0)
    //        {
    //            Death();
    //        }
    //    }
    //}

    //  public Vector2 Size;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        name = PoolName.Boss;
        moveRange = 1;
        id = -1;
        EventCenter.GetInstance().AddEventListener("BossDead", () =>
        {
            Death();
            isWaitDead = false;
            animator.SetBool("WaitDead", isWaitDead);


            //隐藏血条
            UIManager.GetInstance().HidePanel("BossHpPanel");
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            if (!isWaitDead&&!is_Death)
            {
                //if (HP > 499)
                //    HP -= 2;
                //计算与主角的位置
                float x = this.transform.position.x - player.transform.position.x;
                //朝相
                if (x > 0 && this.rigidbody.velocity.x <= 0)
                {
                    faceDir = -1;
                    this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 180, this.transform.eulerAngles.z);
                }
                else if (x > 0 && this.rigidbody.velocity.x > 0)
                {
                    //超过范围就往回走
                    if (x > 5f)
                    {
                        faceDir = -1;
                        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 180, this.transform.eulerAngles.z);
                    }

                    this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 0, this.transform.eulerAngles.z);
                }
                if (x < 0 && this.rigidbody.velocity.x >= 0)
                {
                    faceDir = 1;
                    this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 0, this.transform.eulerAngles.z);
                }
                else if (x < 0 && this.rigidbody.velocity.x < 0)
                {
                    //超过范围就往回走
                    if (x < -5f)
                    {
                        faceDir = 1;
                        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 0, this.transform.eulerAngles.z);
                    }

                    this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 180, this.transform.eulerAngles.z);
                }

                //移动
                moveTime += Time.deltaTime;

                if (!isHurt)
                {
                    if (!HeiShening)
                    {
                        //朝角色走
                        if (Mathf.Abs(x) > moveRange && !isRotate)
                            rigidbody.velocity = new Vector2(faceDir * speed, rigidbody.velocity.y);
                        else
                        {

                            if (moveTime > Random.Range(4, 8))
                            {
                                //朝反方向走
                                isRotate = true;
                                //重置
                                Invoke("Rotated", 1.6f);
                                moveTime = 0;
                            }

                        }
                        //往反方向走
                        if (isRotate && Mathf.Abs(x) < 5 && !animator.GetBool("SpecialAtk"))
                        {
                            rigidbody.velocity = new Vector2(-faceDir * speed, rigidbody.velocity.y);
                        }
                    }
                    else
                    {
                        rigidbody.velocity = Vector2.zero;
                    }     //播放移动动画
                    if (Mathf.Abs(rigidbody.velocity.x) > 0)
                    {
                        animator.SetBool("Run", true);
                    }
                    else
                    {
                        animator.SetBool("Run", false);
                    }
                }
            

                //print(rigidbody.velocity.x);
           
                AtkTime += Time.deltaTime;
                //攻击
                if (Mathf.Abs(x) <= 1 && AtkTime > AtkSpeed)
                {
                    animator.SetTrigger("Atk");
                    AtkTime = 0;
                }
                SpecialTime += Time.deltaTime;
                //特殊攻击
                if (((x >= atkDis && faceDir == -1 && this.transform.eulerAngles.y == 180) || (x <= -atkDis && faceDir == 1 && this.transform.eulerAngles.y == 0)) && (SpecialTime > SpecialSpeed))
                {
                    animator.SetBool("SpecialAtk", true);
                    //记录起始位置
                    SpecialVector3 = this.transform.position;
                    nowFace = faceDir;
                    //rigidbody.velocity = Vector2.zero;
                    //rigidbody.AddRelativeForce(new Vector2(500, 0),ForceMode2D.Impulse);
                    SpecialTime = 0;
                }
                //瞬移
                if (animator.GetBool("SpecialAtk"))
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, this.transform.position + new Vector3(8, 0) * faceDir, Time.deltaTime * 1.5f);
                }
                //判断是否撞到了墙
                RaycastHit2D info = Physics2D.Raycast(this.transform.position, Vector2.right * -faceDir, 0.2f, 1 << LayerMask.NameToLayer("AirWall"));
                if (info.transform != null)
                {
                    rigidbody.velocity = new Vector2(faceDir * speed, rigidbody.velocity.y);
                }
                //Debug.DrawRay(this.transform.position,Vector2.down *f);
               /* RaycastHit2D ray = Physics2D.Raycast(this.transform.position, Vector2.down, 1, 1 << LayerMask.NameToLayer("Ground"));
                if (ray.transform != null)
                {
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
                    rigidbody.gravityScale = 0;
                }
                else
                {
                    rigidbody.gravityScale = 1;
                }*/
                //跳跃
                // if()
                // animator.SetTrigger("Jump");
            }
        }
    
    }
    /// <summary>
    /// 普通攻击
    /// </summary>
    public void AtkEvent()
    {
        Collider2D collider2D = Physics2D.OverlapBox(this.transform.position + new Vector3(0.91f, -0.09f) * faceDir, new Vector2(1.18f, 0.28f), 0, 1 << LayerMask.NameToLayer("Player"));
        if (collider2D != null)
        {
            collider2D.GetComponent<Player>().Hurt(Atk);
        }
           
    }
    public void SpecialAtkEnd()
    {
        StartCoroutine(SpecialAtk());  
    }
    /// <summary>
    /// 特殊攻击
    /// </summary>
    /// <returns></returns>
    IEnumerator SpecialAtk()
    {

        animator.SetBool("SpecialAtk", false);
        yield return new WaitForSeconds(0.2f);
        Vector3 nowPos = this.transform.position;
        Vector3 Pos;
        Vector2 Size = new Vector2(Mathf.Abs(nowPos.x - SpecialVector3.x), 1);
        // 定义绘制的中心点
        Vector3 s = new Vector3(Mathf.Abs((nowPos.x - SpecialVector3.x) / 2), Mathf.Abs((nowPos.y - SpecialVector3.y) / 2), 0);
        //从左往右冲
        if (nowFace == 1)
        {
            Pos = SpecialVector3 + s;
        }
        //从右往左冲
        else
        {
            Pos = SpecialVector3 - s;
        }

        //生成剑气
        PoolMgr.GetInstance().GetObj(PoolName.SwordQi, (obj) =>
        {
            //设置剑气的位置
            obj.transform.position = Pos;
            //设置剑气的判定范围
            obj.GetComponent<SwordQi>().Size = Size;
            //设置剑气的大小
            obj.transform.localScale = new Vector3(obj.transform.localScale.x, Mathf.Abs(nowPos.x - SpecialVector3.x) *2.15f, 0);
            //设置剑气的方向
            if(nowFace == 1)
            obj.GetComponent<SpriteRenderer>().flipY = false;
            else
                obj.GetComponent<SpriteRenderer>().flipY = true;
        });
        //生成判定
        Collider2D collider2D = Physics2D.OverlapBox(Pos, Size, 0, 1 << LayerMask.NameToLayer("Player"));

        //让角色受伤
        if (collider2D != null)
            collider2D.GetComponent<Player>().Hurt(Atk * 2);

        Vector3 add = new Vector3(Mathf.Abs(nowPos.x - SpecialVector3.x) / 5, 0, 0);
        if (isHeiShen)
        {
            //生成炸弹
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = PoolMgr.GetInstance().GetObj(PoolName.FireBall);
                //设置炸弹的偏移
                if(nowFace == 1)
                obj.transform.position = SpecialVector3 + add * (i + 1);
                else
                    obj.transform.position = SpecialVector3 - add * (i + 1);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
    //void OnDrawGizmos()
    //{
    //    Vector3 center = SpecialVector3;


    //    //绘制框形状
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(center, new Vector3(0.8f,0.8f,0));
    //}
    private void HeiShenEnd()
    {
        this.GetComponent<SpriteRenderer>().material.color = new Color(0.9499347f, 0.1254902f, 1);
        //print(this.GetComponent<SpriteRenderer>());
        HeiShening = false;
    }
    private void Rotated()
    {
        isRotate = false;
    }
   
}
