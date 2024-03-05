using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SureSavePanel : BasePanel
{
    //那个存档
    public int id;
    public bool isXin;
    public override void ShowMe()
    {
        this.gameObject.SetActive(true);
    }
    public override void HideMe()
    {
        this.gameObject.SetActive(false);
    }
    protected override void OnClick(string btnName)
    {
        MusicMgr.GetInstance().PlaySound("Click");
        switch (btnName)
        {

            case "btnSure":
                if (isXin)
                {
                    //保存游戏
                    GameFacade.GetInstance().SendNotification(CommandName.SAVE_PLAYERMODEL, id,isXin.ToString());
                    //进入村庄
                    GameFacade.GetInstance().SendNotification(CommandName.ENTER_SCENE,id, CommandName.COUNTRY_SCENE);
                }
                else
                    //保存游戏
                    GameFacade.GetInstance().SendNotification(CommandName.SAVE_PLAYERMODEL,id);

                UIManager.GetInstance().HidePanel("SureSavePanel");
                break;
            case "btnCancel":
                UIManager.GetInstance().HidePanel("SureSavePanel");
                break;
        }
    }
}
