using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlayerModelCommand :SimpleCommand
{
 
    public override void Execute(INotification notification)
    {
        SaveModelProxy Saveproxy;
        SaveModel saveModel;
        int id;
        Saveproxy = Facade.RetrieveProxy(SaveModelProxy.NAME) as SaveModelProxy;
        //记录当前的存档框
        if (notification.Body != null)
            id = (int)notification.Body;
        else
            id = Saveproxy.NowId; 

        //新创的场景
        if (notification.Type != null)
        {
            saveModel = new SaveModel();
            saveModel.hatModels = JsonMgr.Instance.LoadData<List<HatModel>>("Json/HatInfo");
        }
          
        //保存已有信息
        else
            saveModel = Saveproxy.ReturnModel(Saveproxy.NowId);

        //saveModel.level = SceneManager.GetActiveScene().name.Split('.')[1];
        saveModel.time = DateTime.Now.ToString("f");
        saveModel.oldSoul = saveModel.soul;
        //saveModel.sceneName = SceneManager.GetActiveScene().name;
        Saveproxy.AddSaveModel(id, saveModel);
        Saveproxy.Save();
        if(notification.Type == null)
        //如果是保存了已有信息则更新面板并显示
        SendNotification(CommandName.UPDATE_SAVEPANEL,id);
    }
}
