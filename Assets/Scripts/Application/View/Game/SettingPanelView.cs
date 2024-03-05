using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPanelView : BasePanel
{
    public override void HideMe()
    {
        this.gameObject.SetActive(false);
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
            case "btnExit":
            case "btnCon":
                UIManager.GetInstance().HidePanel("SettingPanel");
                Time.timeScale = 1;
                break;
            case "btnSet":
                UIManager.GetInstance().ShowPanel<MusicPanel>("MusicPanel");
                break;
            case "btnReturnMain":
                //切换回开始场景
                GameFacade.GetInstance().SendNotification(CommandName.ENTER_SCENE,type:CommandName.START_SCENE);
                Time.timeScale = 1;
                break;
            case "btnQuit":
                Application.Quit();
                break;
            case "btnSave":
                GameFacade.GetInstance().SendNotification(CommandName.SHOW_SAVEPANEL, false);
                break;
        }
    }
}
