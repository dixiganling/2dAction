using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBullet :MonoBehaviour
{
    public int atk;
    private Player player;
    public Vector3 atkDir;
    private float spe_Time;
    public GameObject son_Bullet;
    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();


    }

    // Update is called once per frame
    void Update()
    {
        spe_Time += Time.deltaTime;
        this.transform.Translate(atkDir.normalized*Time.deltaTime);
        if (spe_Time >= 4)
        {
            atkDir = player.transform.position - this.transform.position;
            //生成3个不同方位的子弹
            for (int i = -1; i <= 1; i++)
            {
                Vector3 vector3 = Quaternion.AngleAxis(45 * i, Vector3.forward) * atkDir.normalized;
                PoolMgr.GetInstance().GetObj(PoolName.SonBullet, (obj)=> {
                    obj.transform.position = this.transform.position;
                    obj.GetComponent<Bullet>().dir = vector3;
                });



            }
            spe_Time = 0;
            PoolMgr.GetInstance().PushObj(PoolName.BigBullet, this.gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //角色受伤
            collision.GetComponent<Player>().Hurt(atk);
            //销毁自己
            PoolMgr.GetInstance().PushObj(PoolName.BigBullet, this.gameObject);
        }
    }

    
}
