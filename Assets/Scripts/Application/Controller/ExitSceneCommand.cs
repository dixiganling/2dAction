using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSceneCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        switch (notification.Type)
        {

            case CommandName.START_SCENE:
                //取消注册
                Facade.RemoveMediator(StartPanelMediator.NAME);
                Facade.RemoveMediator(SavePanelMediator.NAME);
                Facade.RemoveProxy(CreateMonstersProxy.NAME);
                //移除ui
                UIManager.GetInstance().HidePanel("StartPanel");
               
                break;
            case CommandName.COUNTRY_SCENE:
                Facade.RemoveMediator(SavePanelMediator.NAME);
                Facade.RemoveMediator(MainPanelMediator.NAME);
                Facade.RemoveProxy(CreateMonstersProxy.NAME);
                UIManager.GetInstance().HidePanel("MainPanel");
                UIManager.GetInstance().HidePanel("SettingPanel");
                UIManager.GetInstance().HidePanel("ControlPanel");

                break;
            case CommandName.GAME_SCENE1_1:
            case CommandName.GAME_SCENE1_2:
            case CommandName.GAME_SCENE1_3:
            case CommandName.GAME_SCENE1_4:
            case CommandName.GAME_SCENE1_5:
            case CommandName.GAME_SCENE1_6:
            case CommandName.GAME_SCENE1_7:
            case CommandName.GAME_SCENE1_8:
            case CommandName.GAME_SCENE1_9:
            case CommandName.GAME_SCENE1_10:
                //取消注册
                //Facade.RemoveMediator(GamePanelMediator.NAME);
                //Facade.RemoveCommand(CommandName.SHOW_SETTING);
                Facade.RemoveProxy(CreateMonstersProxy.NAME);
                Facade.RemoveMediator(MainPanelMediator.NAME);
                //移除ui
                //UIManager.GetInstance().HidePanel("MainPanel");
                UIManager.GetInstance().HidePanel("SettingPanel");
                UIManager.GetInstance().GetPanel<ChaPanelView>("ChaPanel").HideMe();
                UIManager.GetInstance().HidePanel("DeadPanel");
                UIManager.GetInstance().HidePanel("ControlPanel");
                UIManager.GetInstance().HidePanel("BossHpPanel");
                //清空池子
                PoolMgr.GetInstance().Clear();
                break;
        }
    }
}
