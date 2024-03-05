using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private float time;
    private float dieTime = 2;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= dieTime)
        {
            animator.SetTrigger("Death");
        }
     
    }
    private void Dead()
    {
        time = 0;
        PoolMgr.GetInstance().PushObj(PoolName.FireBall,this.gameObject);
    }
    private void Atk()
    {
        //产生爆炸伤害
        Collider2D collider2D = Physics2D.OverlapCircle(this.transform.position, 0.8f, 1 << LayerMask.NameToLayer("Player"));
        if (collider2D != null)
        {
            collider2D.GetComponent<Player>().Hurt(5);
        }
    }
   
  

   
 
}
