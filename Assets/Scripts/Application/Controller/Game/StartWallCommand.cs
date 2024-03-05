using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWallCommand : SimpleCommand
{
    private DialogModelProxy proxy;
    public override void Execute(INotification notification)
    {
        
        proxy = Facade.RetrieveProxy(DialogModelProxy.NAME) as DialogModelProxy;
        switch (notification.Type)
        {
            case "Dialog":
                //显示ui
                UIManager.GetInstance().ShowPanel<DialogPanel>("DialogPanel", E_UI_Layer.System, (panel) => {
                    panel.model = proxy.GetModel().Start[Random.Range(0, proxy.GetModel().Start.Count)];
                });
                break;
            case "dialog":
                //显示对话
                UIManager.GetInstance().ShowPanel<dialogPanel1>("dialogPanel1", E_UI_Layer.System,(panel)=> {
                    panel.model = proxy.GetModel().End[Random.Range(0, proxy.GetModel().End.Count)];
                    panel.isBoss = (bool)notification.Body;
                });
                break;
        }
    

 
    }
}
