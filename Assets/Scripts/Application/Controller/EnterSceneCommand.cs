using Cinemachine;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterSceneCommand : SimpleCommand
{
    SaveModelProxy saveProxy;
    PlayerModelProxy playerModelProxy;
    MusicProxy musicProxy;
    public override void Execute(INotification notification)
    {

      
        //记录当前的存档信息
        if (notification.Body!= null)
        {
            saveProxy = Facade.RetrieveProxy(SaveModelProxy.NAME) as SaveModelProxy;
            
            saveProxy.NowId = (int)notification.Body;
        }
        //清除音频
        MusicMgr.GetInstance().Clear();
        //SaveModel saveModel;
        //List<object> list = notification.Body as List<object>;
        //if (list[1] is SaveModel)
        //{
        //    saveModel = list[1] as SaveModel;
        //}
        //else
        //    saveModel = null;
        //退出当前场景
        //if(notification.Body!=null)
        //SendNotification(CommandName.EXIT_SCENE, type:notification.Body.ToString());
        SendNotification(CommandName.EXIT_SCENE, type: SceneManager.GetActiveScene().name);
        //进入对应的场景
        ScenesMgr.GetInstance().LoadSceneAsyn(notification.Type, () => {
            switch (notification.Type)
            {
                //根据不同的场景注册不同的事件
                case CommandName.START_SCENE:
                    MusicMgr.GetInstance().PlayBkMusic("Start");
                    if (!Facade.HasMediator(StartPanelMediator.NAME))
                    {
                        //加载ui
                        UIManager.GetInstance().ShowPanel<StartPanelView>("StartPanel", callBack: (panel) => {
                            Facade.RegisterMediator(new StartPanelMediator(panel));
                    });
                    }
                 
                    break;
                case CommandName.COUNTRY_SCENE:
                    musicProxy = Facade.RetrieveProxy(MusicProxy.NAME) as MusicProxy;
                    MusicMgr.GetInstance().PlayBkMusic("Country");
                    //重置角色信息
                    Facade.RemoveProxy(PlayerModelProxy.NAME);
                    //重置mainPanel
                    GameFacade.GetInstance().RemoveMediator(MainPanelMediator.NAME);
                    //重置ChaPanel
                    UIManager.GetInstance().HidePanel("ChaPanel");
                    //注册帽子哥
                    if (!Facade.HasCommand(CommandName.SHOW_HAT))
                    {
                        Facade.RegisterCommand(CommandName.SHOW_HAT, () =>
                        {
                            return new ShowHatCommand();
                        });
                    }
                    //if (!Facade.HasProxy(PlayerModelProxy.NAME))
                    //{
                    //    Facade.RegisterProxy(new PlayerModelProxy());
                    //}
                    //if (playerModelProxy == null)
                    //    playerModelProxy = Facade.RetrieveProxy(PlayerModelProxy.NAME) as PlayerModelProxy;



                    ResMgr.GetInstance().LoadAsync<GameObject>(PoolName.Player, (obj) =>
                    {
                        Player player = obj.GetComponent<Player>();
                        //设置角色信息
                        player.playerModel = new PlayerModel();
                        // player.playerModel = new PlayerModel();
                        player.playerModel.control = musicProxy.GetMusicModel().control;
                        //设置角色位置
                        obj.transform.position = GameObject.Find("PlayerPos").transform.position;
                        //设置摄像机的跟随对象
                        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = obj.transform;
                        //ui
                        UIManager.GetInstance().ShowPanel<ControlPanel>("ControlPanel", callBack: (view) => {
                            view.player = player;
                            view.DisableBtn();
                        });
                        if (!Facade.HasMediator(MainPanelMediator.NAME))
                        {
                            UIManager.GetInstance().ShowPanel<MainPanelView>("MainPanel", E_UI_Layer.Mid, (view) => {
                                view.isSave = true;
                                Facade.RegisterMediator(new MainPanelMediator(view));
                            });
                        }
                
                    });

                    break;
                case CommandName.GAME_SCENE1_1:
                case CommandName.GAME_SCENE1_2:
                case CommandName.GAME_SCENE1_3:
                    EnterGameScene();
                    MusicMgr.GetInstance().PlayMainBkMusic("Scene1");
                    break;
                case CommandName.GAME_SCENE1_4:
                case CommandName.GAME_SCENE1_5:
                case CommandName.GAME_SCENE1_6:
                    EnterGameScene();
                    MusicMgr.GetInstance().PlayMainBkMusic("Scene2");
                    break;
                case CommandName.GAME_SCENE1_7:
                case CommandName.GAME_SCENE1_8:
                case CommandName.GAME_SCENE1_9:
        
                    //注册相关
                    /*Facade.RegisterCommand(CommandName.SHOW_SETTING,()=> {
                        return new ShowSettingPanelCommand();
                    });*/

                    EnterGameScene();
                    MusicMgr.GetInstance().PlayMainBkMusic("Scene3");
                    break;
                case CommandName.GAME_SCENE1_10:
                    EnterGameScene();
                    //创建boss
                    PoolMgr.GetInstance().GetObj(PoolName.Boss,(obj)=> {
                        obj.transform.position = GameObject.Find("BossPos").transform.position;
                       
                    });
                    MusicMgr.GetInstance().PlayMainBkMusic("Scene3");
                    break;
            }
        });
      

      
    
    }
    /// <summary>
    /// 进入游戏场景
    /// </summary>
    public void EnterGameScene()
    {
        if (!Facade.HasProxy(PlayerModelProxy.NAME))
        {
            Facade.RegisterProxy(new PlayerModelProxy());
        }
        if (playerModelProxy == null)
            playerModelProxy = Facade.RetrieveProxy(PlayerModelProxy.NAME) as PlayerModelProxy;
        //创建角色
        ResMgr.GetInstance().LoadAsync<GameObject>(PoolName.Player, (obj) => {
            Player player = obj.GetComponent<Player>();
            //设置角色信息
            player.playerModel = playerModelProxy.ReturnModel();
            //设置角色位置
            obj.transform.position = GameObject.Find("PlayerPos").transform.position;



            //设置摄像机的跟随对象
            GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = obj.transform;
      
            UIManager.GetInstance().ShowPanel<ChaPanelView>("ChaPanel", callBack: (view) => {
                view.player = player;
            });
            UIManager.GetInstance().ShowPanel<ControlPanel>("ControlPanel", callBack: (view) => {
                view.player = player;
            });
            if (!Facade.HasMediator(MainPanelMediator.NAME))
            {
                UIManager.GetInstance().ShowPanel<MainPanelView>("MainPanel", E_UI_Layer.Mid, (view) => {
                    view.isSave = false;
                    Facade.RegisterMediator(new MainPanelMediator(view));
                });
            }
        });

    }
}
