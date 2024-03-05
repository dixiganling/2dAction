using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRewardCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        int id = (int)notification.Body;
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        switch (id)
        {
            //最大精力+20
            case 1:
                player.MaxEnergy += 20;
                player.Energy += 20;
                break;
            //最大生命+40
            case 2:
                player.MaxHp += 40;
                player.Hp += 40;
                break;
            //移动速度+0.3
            case 3:
                player.playerModel.speed += 0.3f;
                break;
            //生成闪电
            case 4:
                player.playerModel.lighting = true;
                break;
            //吸血
            case 5:
                player.playerModel.xiXue = true;
                break;
            //攻击力+3
            case 6:
                player.playerModel.atk += 3;
                break;
            //吸血值+2
            case 7:
                player.playerModel.xiXueValue += 2;
                break;
            //闪电数+1
            case 8:
                player.playerModel.lightingNum += 1;
                break;
            //闪电伤害+5
            case 9:
                player.playerModel.lightingHurt += 5;
                break;
            //灵魂数量+40
            case 10:
                SendNotification(CommandName.CHANGE_DATA,40,"soul");
                break;
        }
        //取消注册RewardPanelView面板
        Facade.RemoveMediator(RewardPanelMediator.NAME);
    }
}
