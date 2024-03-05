using PureMVC.Patterns.Proxy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogModelProxy : Proxy
{
    public new static string NAME = "DialogModelProxy";
    private DialogModel dialogModel;
    public DialogModelProxy( object data = null) : base(NAME, data)
    {
        dialogModel = JsonMgr.Instance.LoadData<DialogModel>("Json/DialogInfo");
    }
    public DialogModel GetModel()
    {
        return dialogModel;
    }
}
