using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAirWallsCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GameObject m = GameObject.FindGameObjectWithTag("Monster");
        if (m == null)
        {
            //停止音乐
            MusicMgr.GetInstance().StopBKMusic();
            //播放胜利音乐
            MusicMgr.GetInstance().PlaySound("Victory");
            GameObject[] obj = GameObject.FindGameObjectsWithTag("FireWall");
            //删掉空气墙
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i].GetComponent<Animator>().SetTrigger("Death");
            }
            //生成奖励
            if(!Facade.HasMediator(RewardPanelMediator.NAME))
            {
                UIManager.GetInstance().ShowPanel<RewardPanelView>("RewardPanel", E_UI_Layer.Top, (view) =>
                {
                    Facade.RegisterMediator(new RewardPanelMediator(view));
                });
                Time.timeScale = 0;
            }
        }
        
    }
}
