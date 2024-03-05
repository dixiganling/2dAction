using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSavePanelCommand : SimpleCommand
{
    public SavePanelMediator mediator;
    public override void Execute(INotification notification)
    {
        if (!Facade.HasMediator(SavePanelMediator.NAME))
        {
            UIManager.GetInstance().ShowPanel<SavePanel>("SavePanel", E_UI_Layer.Top, (obj) => {
                Facade.RegisterMediator(mediator = new SavePanelMediator((bool)notification.Body, obj));
              
            });
        }
    }
}
