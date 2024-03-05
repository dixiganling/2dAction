using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanelMediator : Mediator
{
    public static new string NAME = "StartPanelMediator";
    public StartPanelView panelView;
    private List<object> obj = new List<object>();
    public StartPanelMediator (object viewComponent = null) : base(NAME, viewComponent)
    {
        panelView = viewComponent as StartPanelView;
        //为控件按钮添加监听事件
        Click();
        obj.Add(SceneManager.GetActiveScene().name);
        obj.Add(0);
    }
    /// <summary>
    /// 添加监听事件
    /// </summary>
    public void Click()
    {
        //创建存档
        panelView.GetControl<Button>("btnPlay").onClick.AddListener(()=> {
            MusicMgr.GetInstance().PlaySound("Click");
            SendNotification(CommandName.SHOW_SAVEPANEL,true);
        });
        panelView.GetControl<Button>("btnExit").onClick.AddListener(() => {
            MusicMgr.GetInstance().PlaySound("Click");
            Application.Quit();
        });
        panelView.GetControl<Button>("btnSet").onClick.AddListener(() =>
        {
            MusicMgr.GetInstance().PlaySound("Click");
            //显示设置面板
            UIManager.GetInstance().ShowPanel<MusicPanel>("MusicPanel");
        });
        //panelView.GetControl<Button>("btnCon").onClick.AddListener(() =>
        //{
        //    //显示存档面板
        //    SendNotification(CommandName.Start_Game,false);
        //});
    }
}
