using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum E_Control
{
    computer,
    phone
}
public class Player : MonoBehaviour
{
    public PlayerModel playerModel;
    //public float speed;
    public float roll_Speed;
    public float jump_Force;
    public bool is_Death = false;
    public bool is_Poisoning = false;

    //public int atk;
    //public float  maxHp = 100;
    //private float hp;
    //public float  maxEnergy = 100;
    //private float energy;
    //public float addEnergySpeed = 10;
    //public bool lighting;
    //public int lightingNum = 1;

    //public int xiXueValue = 1;
    //public bool xiXue;
    public float Hp
    {
        get
        {
            return playerModel.hp;
        }
        set
        {
            playerModel.hp = Mathf.Clamp(value, 0, MaxHp);
            if (playerModel.hp <= 0)
            {
                Death();
            }
        }
    }
    public float Energy
    {
        get
        {
            return playerModel.energy;
        }
        set
        {
            playerModel.energy = Mathf.Clamp(value, 0, playerModel.maxEnergy);
        }
    }
    public float MaxHp {
        get
        {
            return playerModel.maxHp;
        }
        set
        {
            playerModel.maxHp = value;
            float add = playerModel.maxHp / 100;
            UIManager.GetInstance().GetPanel<ChaPanelView>("ChaPanel").ChangeHpWidth(add);
        }
    }

    public float  MaxEnergy
    {
        get
        {
            return playerModel.maxEnergy;
        }
        set
        {

            playerModel.maxEnergy = value;
            float add = playerModel.maxEnergy / 100;
            UIManager.GetInstance().GetPanel<ChaPanelView>("ChaPanel").ChangeEneWidth(add);
        }
    }
    //从墙上掉落时的灰尘特效
    public GameObject slideDust;

    private Animator animator;
    public Rigidbody2D rigidbody;
   // private BoxCollider2D boxCollider;
    private CapsuleCollider2D capsuleCollider;
    public CircleCollider2D circleCollider;
    private EdgeCollider2D atk1;
    private EdgeCollider2D atk2;
    private EdgeCollider2D atk3;
    private PlayerSensor groundSensor;
    private PlayerSensor wallSensorR1;
    private PlayerSensor wallSensorR2;
    private PlayerSensor wallSensorL1;
    private PlayerSensor wallSensorL2;
    private SpriteRenderer sprite;
    //人脸朝相，左为1，右为-1
    private int faceDir = 1;
    //判断是否在翻滚
    public bool is_Roll = false;
    //判断是否在地面
    private bool is_Ground = true;
    //攻击间隔时间
    private float atkTime = 0;
    //攻击移动距离
    public float attackMovedis = 1;
    //攻击动画标识
    private int atkId = 0;
    private string atkname;
    //闪电起始位置
    private Vector3 lightingV;
    //手机操控标识
    public float InputX;
    public bool Jump;
    public bool Atk;
    public bool Roll;
    public bool isAtk;
    public bool isMove;
    //int i = 100;
    void Start()
    {
        //if (playerModel == null)
        //    JsonMgr.Instance.LoadData<PlayerModel>("PlayerModel");
        //GameFacade.GetInstance().RetrieveProxy();
        animator = this.GetComponent<Animator>();
        rigidbody = this.GetComponent<Rigidbody2D>();
       // boxCollider = this.GetComponent<BoxCollider2D>();
        capsuleCollider = this.GetComponent<CapsuleCollider2D>();
        circleCollider = this.GetComponent<CircleCollider2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        atk1 = this.GetComponentsInChildren<EdgeCollider2D>()[0];
        atk2 = this.GetComponentsInChildren<EdgeCollider2D>()[1];
        atk3 = this.GetComponentsInChildren<EdgeCollider2D>()[2];
        groundSensor = this.transform.Find("GroundSensor").GetComponent<PlayerSensor>();
        wallSensorR1 = this.transform.Find("WallSensor_R1").GetComponent<PlayerSensor>();
        wallSensorR2 = this.transform.Find("WallSensor_R2").GetComponent<PlayerSensor>();
        wallSensorL1 = this.transform.Find("WallSensor_L1").GetComponent<PlayerSensor>();
        wallSensorL2 = this.transform.Find("WallSensor_L2").GetComponent<PlayerSensor>();

        capsuleCollider.enabled = true;
        circleCollider.enabled = false;

        //Energy = playerModel.maxEnergy;
        //Hp = playerModel.maxHp;

        atk1.enabled = false;
        atk2.enabled = false;
        atk3.enabled = false;
    }
    void Update()
    {
        if (!is_Death)
        {
            //atk1.enabled = false;
            //atk2.enabled = false;
            //atk3.enabled = false;
            if (!is_Roll)
                Energy += Time.deltaTime * playerModel.addEnergySpeed;
            #region 动画相关   
            //攻击间隔
            atkTime += Time.deltaTime;
            //角色刚落地
            if (!is_Ground && groundSensor.State())
            {
                is_Ground = true;
                animator.SetBool("Ground", is_Ground);
            }

            //角色从高处地板上往下落
            if (is_Ground && !groundSensor.State())
            {
                is_Ground = false;
                animator.SetBool("Ground", is_Ground);
            }
            //移动，检测键盘输入
            if (playerModel.control == E_Control.computer)
            {
                if(!isMove)
                InputX = Input.GetAxis("Horizontal");

                if (Input.GetKeyDown("left shift") && !is_Roll && is_Ground && Energy >= 33)
                {
         
                    //animator.SetBool("Roll", is_Roll);
                    animator.SetTrigger("Roll");
                
                    lightingV = this.transform.position;
                }
                //设置跳跃动画
                //在地面才能跳跃
                else if (Input.GetKeyDown(KeyCode.Space) && !is_Roll && (is_Ground || animator.GetBool("WallSlide")))
                {
                    MusicMgr.GetInstance().PlaySound("Player/Jump");
                    is_Ground = false;
                    animator.SetBool("Ground", is_Ground);
                    //播放跳跃动画
                    animator.SetTrigger("Jump");
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump_Force);
                    //刚起跳
                    groundSensor.Disable(0.2f);
                }
                else if ((Input.GetMouseButtonDown(0))&& !is_Roll && atkTime > 0.3f/*动画间隔*/)
                {
                    atkId++;
                    //if (atkId == 1)
                    //    MusicMgr.GetInstance().PlaySound("Player/Atk/Sound1");
                    //if (atkId == 2)
                    //    MusicMgr.GetInstance().PlaySound("Player/Atk/Sound2");
                    //if (atkId == 3)
                    //    MusicMgr.GetInstance().PlaySound("Player/Atk/Sound3");
                    //播放到第三个动画后重置
                    if (atkId > 3)
                        atkId = 1;
                    //攻击间隔太长后重置动画
                    if (atkTime > 1.0f)
                        atkId = 1;
                    animator.SetTrigger("Attack" + atkId);
                    atkTime = 0;
                }
            }

            //y轴即跳跃时的速度
            animator.SetFloat("AirSpeedY", rigidbody.velocity.y);

            //从墙上滑下来
            //左边/右边碰到墙体
            animator.SetBool("WallSlide", (wallSensorR1.State() && wallSensorR2.State()) || (wallSensorL1.State() && wallSensorL2.State()));
            //设置移动速度
            //移动时不能翻滚
      
            if (!is_Roll && !isAtk)
            {
                rigidbody.velocity = new Vector2(InputX * playerModel.speed, rigidbody.velocity.y);

            }
         
            //设置人物朝相
            if (InputX > 0)
            {
                this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x,0,this.transform.eulerAngles.z);
                //this.GetComponent<SpriteRenderer>().flipX = false;
                faceDir = 1;
            }
            else if (InputX < 0)
            {
                this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 180, this.transform.eulerAngles.z);
                //this.GetComponent<SpriteRenderer>().flipX = true;
                faceDir = -1;
            }
            //设置翻滚动画播放
            //在空中不能翻滚
            if (/*Input.GetKeyDown("left shift")*/Roll && !is_Roll && is_Ground && Energy >= 33)
            {
                //Debug.Log("开始");
                //is_Roll = true;
                animator.SetTrigger("Roll");
             
                //animator.SetBool("Roll", is_Roll);
                //animator.SetTrigger("Roll");
                //可以翻滚了
                   Roll = false;
                lightingV = this.transform.position;
            }
            //设置跳跃动画
            //在地面才能跳跃
            else if (/*(Input.GetKeyDown(KeyCode.Space)||*/ Jump&& !is_Roll && (is_Ground || animator.GetBool("WallSlide")))
            {
                MusicMgr.GetInstance().PlaySound("Player/Jump");
                is_Ground = false;
                animator.SetBool("Ground", is_Ground);
                //播放跳跃动画
                animator.SetTrigger("Jump");
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump_Force);
                //刚起跳
                groundSensor.Disable(0.2f);
                Jump = false;
            }
            //防御动画
            //else if (Input.GetMouseButtonDown(1) && !is_Roll)
            //{
            //    animator.SetTrigger("Block");
            //    animator.SetBool("IdleBlock", true);
            //}
            //else if (Input.GetMouseButtonUp(1))
            //{
            //    animator.SetBool("IdleBlock", false);
            //}
            //攻击动画

            else if (/*(Input.GetMouseButtonDown(0)||*/Atk && !is_Roll && atkTime > 0.5f/*动画间隔*/)
            {
                atkId++;
                //if (atkId == 1)
                //    MusicMgr.GetInstance().PlaySound("Player/Atk/Sound1");
                //if (atkId == 2)
                //    MusicMgr.GetInstance().PlaySound("Player/Atk/Sound2");
                //if (atkId == 3)
                //    MusicMgr.GetInstance().PlaySound("Player/Atk/Sound3");
                //播放到第三个动画后重置
                if (atkId > 3)
                    atkId = 1;
                //攻击间隔太长后重置动画
                if (atkTime > 1.0f)
                    atkId = 1;
                animator.SetTrigger("Attack" + atkId);
                atkTime = 0;
                Atk = false;
            }

            //设置移动动画播放
            else if (InputX != 0/*&&!Roll*/)
            {
                animator.SetBool("Run", true);
               // i++;
            }
            else
            {
                animator.SetBool("Run", false);
            }
            #endregion
            //Debug.Log(animator.GetBool("Run"));
            //Debug.Log(i);
        }



    }
    //private void LateUpdate()
    //{
    //    Debug.Log("late");
    //}
    #region 动画事件
    //void Run()
    //{
    //    Debug.Log("Run");
    //}
    void EnterRoll()
    {
       // Debug.Log("翻滚");
        is_Roll = true;
        //与精力条有关  
        Energy -= 33;
        //设置collider
        circleCollider.enabled = true;

        capsuleCollider.enabled = false;
        //设置速度
        rigidbody.velocity = new Vector2(faceDir * roll_Speed, rigidbody.velocity.y);
    }
    /// <summary>
    /// 翻滚事件结束执行的方法
    /// </summary>
    void AE_ResetRoll()
    {

        is_Roll = false;

        circleCollider.enabled = false;
        capsuleCollider.enabled = true;
      
    }
    void CreateLightingEvent()
    {
        //生成闪电
        if (playerModel.lighting)
        {
            StartCoroutine(CreateLighting());
        }
    }
    
    /// <summary>
    /// 生成灰尘
    /// 根据墙的位置不同生成不同位置的灰尘
    /// </summary>
    void AE_SlideDust()
    {
        Vector3 spawnPos;
        if (faceDir == 1)
            spawnPos = wallSensorR2.transform.position;
        else
            spawnPos = wallSensorL2.transform.position;
        if (slideDust != null)
        {
            GameObject obj = Instantiate(slideDust, spawnPos, gameObject.transform.localRotation) as GameObject;
            obj.transform.localScale = new Vector3(faceDir, 1, 1);
        }
    }
    void AtkStartEvent()
    {
        if (is_Ground)
            isAtk = true;
    }
    void AtkEvent()
    {
        if (is_Ground)
        {
            if (InputX == 0)
            {
                //不按方向键小移动
                rigidbody.velocity = new Vector3(attackMovedis * 3 * faceDir, 0);
            }
            else
            {
                //rigidbody.velocity = new Vector3(0, 0);
                rigidbody.velocity = new Vector3(attackMovedis * 4 * InputX, 0);
            }
        }
       
    }
    void AtkEndEvent()
    {
        isAtk = false;    
    }
    /// <summary>
    /// 重置事件
    /// </summary>
    void ResetEvent()
    {
        if (isAtk)
        {
            isAtk = false;
        }
        if (is_Roll)
        {
            is_Roll = false;
            Debug.Log("aaa");
            circleCollider.enabled = false;
            capsuleCollider.enabled = true;
        }

        //Debug.Log("aa");
    }


    #endregion
    /*  private void OnTriggerEnter2D(Collider2D collision)
      {
          if(collision.tag == "Monster")
          {
              //怪物受伤
              collision.GetComponent<Monster>().Hurt(atk);
          }
      }*/
    /// <summary>
    /// 受伤相关
    /// </summary>
    /// <param name="value"></param>
    public void Hurt(int value)
    {
        //没有翻滚且没有死亡
        if (!is_Roll&&!is_Death)
        {
          
            //播放受伤动画
            animator.SetTrigger("Hurt");
            //角色受伤
            Hp -= value;
        }
    
    }
    /// <summary>
    /// 死亡相关
    /// </summary>
    public void Death()
    {
        is_Death = true;
        animator.SetBool("Death", is_Death);
        UIManager.GetInstance().ShowPanel<DeadPanel>("DeadPanel");
        MusicMgr.GetInstance().PlaySound("GameOver",false);
    }
    /// <summary>
    /// 闪电相关
    /// </summary>
    /// <returns></returns>
    IEnumerator CreateLighting()
    {
        for(int i = 0; i < playerModel.lightingNum; i++)
        {
            GameObject obj =  PoolMgr.GetInstance().GetObj(PoolName.Lighting);
            obj.transform.position =lightingV + new Vector3(0, 3) +new Vector3(1,0)*faceDir*i;
            yield return new WaitForSeconds(0.05f);
        }
            
    }
    public void Poisoning()
    {
        is_Poisoning = true;
        StartCoroutine(PoisoningHurt());
    }
    IEnumerator PoisoningHurt()
    {
        int i = 0;
        sprite.material.color = new Color(0.1539539f, 1, 0);
        while (is_Poisoning)
        {
            i++;
            playerModel.hp -= 3;
            yield return new WaitForSeconds(1);
            if(i >= 5)
            {
                is_Poisoning = false;
                sprite.material.color = new Color(1,1,1);
                break;
            }
        }
    }
}
