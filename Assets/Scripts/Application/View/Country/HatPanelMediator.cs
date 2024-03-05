using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatPanelMediator : Mediator
{
    public new static string NAME = "HatPanelMediator";
    public HatPanelView hatPanel;
    public SaveModelProxy proxy;
    public SaveModel saveModel;
    public HatPanelMediator(object viewComponent = null) : base(NAME, viewComponent)
    {
        hatPanel = viewComponent as HatPanelView;
        proxy = Facade.RetrieveProxy(SaveModelProxy.NAME)as SaveModelProxy;
        saveModel = proxy.ReturnModel(proxy.NowId);
        Init();
    }
    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            CommandName.CHANGE_NUM
        };
    }
    //初始化
    public void Init()
    {
        for (int i = 0; i < hatPanel.hatCells.Length; i++)
        {
            hatPanel.hatCells[i].Init(saveModel.hatModels[i],saveModel);
        }
        hatPanel.GetControl<Text>("txtSoul").text = saveModel.soul.ToString();
        OnClick();
    }
    public void OnClick()
    {
        hatPanel.GetControl<Button>("btnExit").onClick.AddListener(() =>
        {
            MusicMgr.GetInstance().PlaySound("Click");
            Facade.RemoveMediator(NAME);
        });
    }
    public override void OnRemove()
    {
        UIManager.GetInstance().HidePanel("HatPanel");
   
        MusicMgr.GetInstance().PlaySound("Hat/Close" + Random.Range(1, 4).ToString(),false);
    }
    //更新灵魂数
    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {
            case CommandName.CHANGE_NUM:
                hatPanel.Change(saveModel.soul.ToString());
                break;
        }
    }
}
