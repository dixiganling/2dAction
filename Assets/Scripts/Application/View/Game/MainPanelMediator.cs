using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelMediator : Mediator
{
    public new static string NAME = "MainPanelMediator";
    public SaveModelProxy saveProxy;
    private MainPanelView panelView;
    private SaveModel saveModel;
    public MainPanelMediator( object viewComponent = null) : base(NAME, viewComponent)
    {
        saveProxy = Facade.RetrieveProxy(SaveModelProxy.NAME) as SaveModelProxy;
        saveModel = saveProxy.ReturnModel(saveProxy.NowId);
        panelView = viewComponent as MainPanelView;
        Change();
    }
    //修改ui
    public void Change()
    {
        panelView.Change(saveModel.soul.ToString());
    }
    public override string[] ListNotificationInterests()
    {
        return new string[]
        {
            CommandName.CHANGE_NUM,
        };
    }
    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {
            case CommandName.CHANGE_NUM:
                Change();
                break;
        }
    }
    public override void OnRemove()
    {
        UIManager.GetInstance().HidePanel("MainPanel");
    }
}
