using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePanelMediator : Mediator
{
    public new static string NAME = "SavePanelMediator";
    public SavePanel savePanel;
    public SaveModelProxy saveProxy;
    public SavePanelMediator( bool isLoad, object viewComponent = null) : base(NAME, viewComponent)
    {
        saveProxy = Facade.RetrieveProxy(SaveModelProxy.NAME) as SaveModelProxy;
        savePanel = viewComponent as SavePanel;
        Init(isLoad);
        savePanel.GetControl<Button>("btnExit").onClick.AddListener(() =>
        {
            MusicMgr.GetInstance().PlaySound("Click");
            Facade.RemoveMediator(NAME);
        });
    }
    public void Init(bool isCreate)
    {
        for (int i = 0; i < savePanel.savePanels.Length; i++)
        {

            //加载存档
            savePanel.savePanels[i].saveModel = saveProxy.ReturnModel(savePanel.savePanels[i].id);
            //开始游戏
            if (isCreate)
            {
                //有存档
                if(savePanel.savePanels[i].saveModel != null)
                {
                    //直接加载场景
                    savePanel.savePanels[i].load.raycastTarget = true;
                    savePanel.savePanels[i].isXin =false;
                }
                //空存档
                else
                {
                    //询问是否选择该存档
                    savePanel.savePanels[i].load.raycastTarget = false;
                    savePanel.savePanels[i].isXin = true;
                }
            }
            //保存游戏
            else
            {
                savePanel.savePanels[i].load.raycastTarget = false;
                savePanel.savePanels[i].isXin = false;
            }
        }
    

    }

    public override void OnRemove()
    {
        UIManager.GetInstance().HidePanel("SavePanel");
    }
    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            CommandName.UPDATE_SAVEPANEL
        };
    }
    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {
            case CommandName.UPDATE_SAVEPANEL:
                Init(false);
                    savePanel.savePanels[(int)notification.Body-1].Load();                
                    break;
        }
    }
}
