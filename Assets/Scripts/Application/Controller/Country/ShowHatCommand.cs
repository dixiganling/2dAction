using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHatCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        if (!Facade.HasMediator(HatPanelMediator.NAME))
        {
            UIManager.GetInstance().ShowPanel<HatPanelView>("HatPanel", E_UI_Layer.Top, (obj) => {
                Facade.RegisterMediator(new HatPanelMediator(obj));

            });
        }
    }
}
