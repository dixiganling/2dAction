using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWall : MonoBehaviour
{
    public Transform[] pos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
            //显示对话窗口
            GameFacade.GetInstance().SendNotification(CommandName.START_WALL,type:"Dialog");
            //创建火墙
            GameFacade.GetInstance().SendNotification(CommandName.CREATE_FIREWALL,pos);
            Destroy(this.gameObject);
        }
    }
}
