using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicPanel : BasePanel
{
    MusicProxy proxy;
    MusicModel model;
    Player player;
    public override void ShowMe()
    {
        this.gameObject.SetActive(true);
        proxy = GameFacade.GetInstance().RetrieveProxy(MusicProxy.NAME) as MusicProxy;
        model = proxy.GetMusicModel();
        //if(SceneManager.GetActiveScene().name.Contains("Start"))
        GameObject obj = GameObject.FindGameObjectWithTag("Player");//.GetComponent<Player>();
        if (obj != null)
            player = obj.GetComponent<Player>();
        GetControl<Slider>("effSound").value = model.effValue;
        GetControl<Slider>("bkSound").value = model.bkValue;
        GetControl<Toggle>("togKey").isOn = model.isOpen;
        GetControl<Slider>("effSound").onValueChanged.AddListener((value) =>
        {
            //修改并保存数据
            model.effValue = value;
            MusicMgr.GetInstance().ChangeSoundValue(value);
        });
        GetControl<Slider>("bkSound").onValueChanged.AddListener((value) =>
        {
            //修改并保存数据
            model.bkValue = value;
            MusicMgr.GetInstance().ChangeBKValue(value);
        });
        
    }
    public override void HideMe()
    {
        this.gameObject.SetActive(false);
        //存储数据
        proxy.SaveData();
    }
    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            //隐藏面板
            case "btnExit":
                MusicMgr.GetInstance().PlaySound("Click");
                UIManager.GetInstance().HidePanel("MusicPanel");
                break;
        }
    }
    protected override void OnValueChanged(string toggleName, bool value)
    {
        switch (toggleName)
        {
            case "togKey":
                model.isOpen = value;
                if (value)
                {

                    model.control = E_Control.computer;
                    if(player != null)
                    player.playerModel.control = E_Control.computer;
                }

                else
                {
                    model.control = E_Control.phone;
                    if (player != null)
                        player.playerModel.control = E_Control.phone;
                }
                        
                break;
        }
    }
}
