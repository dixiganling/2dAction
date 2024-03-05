using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadPanel : BasePanel
{
    public override void HideMe()
    {
        this.gameObject.SetActive(false);
        GameFacade.GetInstance().SendNotification(CommandName.SAVE_PLAYERMODEL);
    }

    public override void ShowMe()
    {
        this.gameObject.SetActive(true);
    }
    protected override void OnClick(string btnName)
    {
        MusicMgr.GetInstance().PlaySound("Click");
        switch (btnName)
        {
            case "btnAgain":
                GameFacade.GetInstance().SendNotification(CommandName.ENTER_SCENE,type:CommandName.COUNTRY_SCENE);
                break;
            case "btnReturn":
                GameFacade.GetInstance().SendNotification(CommandName.ENTER_SCENE,type:CommandName.START_SCENE);
                break;
        }
    }
}
