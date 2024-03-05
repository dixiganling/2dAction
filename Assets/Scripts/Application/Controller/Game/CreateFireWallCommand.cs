using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFireWallCommand : SimpleCommand
{

    public override void Execute(INotification notification)
    {
        Transform[] transforms = notification.Body as Transform[];
        //生成空气墙
        GameObject wall = GameObject.FindGameObjectWithTag("FireWall");
        //同时触发多个
        if (wall == null)
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                GameObject obj = PoolMgr.GetInstance().GetObj(PoolName.AirWall);
                obj.transform.position = transforms[i].position;
            }
        }
    }
}
