using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool is_son;
    public Vector3 dir;
    public int atk;
    private float time;
    private float dieTime = 5;
    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //一定时间后回收
        time += Time.deltaTime;
        if (time >= dieTime)
            PushSelf();

        if (!is_son)
            this.transform.Translate(dir * Random.Range(0.1f, 3)  * Time.deltaTime);
        else
            this.transform.Translate(1 * dir * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player != null)
        {
            if (collision.tag == "Player" && !player.is_Roll)
            {
                //角色受伤
                player.Hurt(atk);
                //销毁自己
                PushSelf();
            }
        }
      
    }

    private void PushSelf()
    {
        time = 0;
        if (!is_son)
            PoolMgr.GetInstance().PushObj(PoolName.Bullet,this.gameObject);
        else
            PoolMgr.GetInstance().PushObj(PoolName.SonBullet, this.gameObject);
    }

}
