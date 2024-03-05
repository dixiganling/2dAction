using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelView : BasePanel
{
    public bool isSave;
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
            case "btnSet":
                UIManager.GetInstance().ShowPanel<SettingPanelView>("SettingPanel",callBack:(obj)=> {
                    Time.timeScale = 0;
                    obj.GetControl<Button>("btnSave").interactable =isSave;
                });
                break;

        }
    }
    public void Change(string txt)
    {
        GetControl<Text>("txtSoul").text = ":" + txt;
    }

}
