using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlPanel : BasePanel
{
    public Player player;
    private bool left;
    private bool right;
    public Button btnEnter;
    public string enterSceneName;
    public string name;
    public override void HideMe()
    {
        this.gameObject.SetActive(false);
    }

    public override void ShowMe()
    {
        this.gameObject.SetActive(true);
        btnEnter = GetControl<Button>("btnEnter");
        btnEnter.gameObject.SetActive(false);
    }
    private void Start()
    {

    }
    private void Update()
    {
        if (left)
        {
            player.InputX = -1;
        }
        if (right)
            player.InputX = 1;
    }
    public void Enter(string name)
    {
        player.Roll = false;
        switch (name)
        {
            case "left":
                left = true;
                break;
            case "right":
                right = true;
                break;
        }
        player.isMove = true;
    }
    public void Exit(string name)
    {
        switch (name)
        {
            case "left":
                left = false;
                break;
            case "right":
                right = false;
                break;
        }
        player.isMove = false;
        player.InputX = 0;
    }
    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "btnJump":
                player.Roll = false;
                player.Jump = true;
                break;
            case "btnAtk":
                player.Jump = false;
                player.Roll = false;
                player.Atk = true;
                break;
            case "btnRoll":
                player.Roll = true;
                break;
            case "btnEnter":
                MusicMgr.GetInstance().PlaySound("Click");
                if (name == "gate") {
                    //进入关卡
                    GameFacade.GetInstance().SendNotification(CommandName.ENTER_SCENE, type: enterSceneName);

                    //保存
                    if(enterSceneName == CommandName.COUNTRY_SCENE)
                    GameFacade.GetInstance().SendNotification(CommandName.SAVE_PLAYERMODEL);
                }
                  
                if (name == "hatman")
                    //显示强化ui
                    GameFacade.GetInstance().SendNotification(CommandName.SHOW_HAT);
                break;
        }
    }
    public void DisableBtn()
    {
        GetControl<Button>("btnRoll").interactable = false;
        GetControl<Button>("btnAtk").interactable = false;
    }
}
