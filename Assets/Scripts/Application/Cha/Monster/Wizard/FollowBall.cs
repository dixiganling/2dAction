using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour
{
    private Player player;
    private int atk = 6;
    private float time;
    private float dieTime = 2.5f;
    private float moveTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= dieTime)
            PushSelf();
 
        //跟踪主角
        this.transform.position = Vector3.Lerp(this.transform.position,player.transform.position + new Vector3(0, 0.5f), moveTime += Time.deltaTime*0.01f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player playerTri = collision.GetComponent<Player>();
        if (playerTri != null)
        {
            if (collision.tag == "Player" )
            {
                //角色受伤
                playerTri.Hurt(atk);
                //销毁自己
                PushSelf();
            }
        }

    }
    private void PushSelf()
    {
        time = 0;
            PoolMgr.GetInstance().PushObj(PoolName.FollowBall, this.gameObject);
    }
}
