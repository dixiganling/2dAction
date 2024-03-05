using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDataCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        SaveModelProxy saveModelProxy = Facade.RetrieveProxy(SaveModelProxy.NAME) as SaveModelProxy;
        switch (notification.Type)
        {
            case "soul":
                saveModelProxy.ChangeSoul((int)notification.Body);
                break;
        }
    }
}
