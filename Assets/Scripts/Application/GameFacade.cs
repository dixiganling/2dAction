using PureMVC.Patterns.Facade;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFacade :Facade
{
    /// <summary>
    /// 实现单例
    /// </summary>
    /// <returns></returns>
    public static GameFacade GetInstance()
    {
        if (instance == null)
            instance = new GameFacade();
        return instance as GameFacade;
    }
    /// <summary>
    /// 启动函数
    /// </summary>
    public void Launch()
    {
        SendNotification(CommandName.ENTER_SCENE,type:CommandName.START_SCENE);
  
       
    }
    protected override void InitializeController()
    {
        base.InitializeController();
        RegisterCommand(CommandName.ENTER_SCENE, () =>
        {
            return new EnterSceneCommand();
        });
        RegisterCommand(CommandName.EXIT_SCENE, () =>
        {
            return new ExitSceneCommand();
        });
        RegisterCommand(CommandName.SAVE_PLAYERMODEL, () =>
        {
            return new SavePlayerModelCommand();
        });
        RegisterCommand(CommandName.CREATE_MONSTERS, () => {
            return new CreateMonstersCommand();
        });
        RegisterCommand(CommandName.DELETE_AIRWALLS, () =>
        {
            return new DeleteAirWallsCommand();
        });
        RegisterCommand(CommandName.GET_REWARD, () =>
        {
            return new GetRewardCommand();
        });
        RegisterCommand(CommandName.SHOW_SAVEPANEL, () =>
        {
            return new ShowSavePanelCommand();
        });
        RegisterCommand(CommandName.CHANGE_DATA, () =>
        {
            return new ChangeDataCommand();
        });
        RegisterCommand(CommandName.START_WALL, () =>
        {
            return new StartWallCommand();
        });
        RegisterCommand(CommandName.CREATE_FIREWALL, () =>
        {
            return new CreateFireWallCommand();
        });
    }
    protected override void InitializeModel()
    {
        base.InitializeModel();
        RegisterProxy(new RewardProxy());
        RegisterProxy(new EncounterProxy());
        RegisterProxy(new SaveModelProxy());
        RegisterProxy(new DialogModelProxy());
        RegisterProxy(new MusicProxy());
    }
}
