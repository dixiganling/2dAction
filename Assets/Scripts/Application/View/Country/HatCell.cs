using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatCell : BasePanel
{
    public HatModel hatModel;
    private SaveModel saveModel;
    public override void HideMe()
    {
        this.gameObject.SetActive(false);
    }
    public override void ShowMe()
    {
        this.gameObject.SetActive(true);
    }
    //初始化
    public void Init(HatModel hatModel,SaveModel saveModel)
    {
        this.saveModel = saveModel;
        this.hatModel = hatModel;
        GetControl<Image>("imgAbility").sprite = ResMgr.GetInstance().Load<Sprite>("UI/Reward/" + hatModel.img);
        GetControl<Text>("txtTips").text = hatModel.tips;
        Init();
    }
    //更新面板
    public void Init()
    {
        GetControl<Text>("txtNow").text = "+:" + hatModel.singleValue * hatModel.nowLevel;
        if (hatModel.nowLevel < hatModel.maxLevel)
        {
            GetControl<Text>("txtNum").text = hatModel.soul[hatModel.nowLevel].ToString();
        }

        else
        {
            GetControl<Text>("txtNum").text = "最大";
            //升级按钮失活
            GetControl<Button>("btnAdd").interactable = false;
        }
         

    }
    protected override void OnClick(string btnName)
    {
      
        switch (btnName)
        {
            case "btnAdd":
                if(saveModel.soul >= hatModel.soul[hatModel.nowLevel])
                {
                    MusicMgr.GetInstance().PlaySound("Click");
                    //减少灵魂
                    GameFacade.GetInstance().SendNotification(CommandName.CHANGE_DATA,-hatModel.soul[hatModel.nowLevel],"soul");
                    //升级
                    if (hatModel.nowLevel < hatModel.maxLevel)
                    {
                        hatModel.nowLevel++;
                    }
                    //更新面板
                    Init();
                }
                else
                {
                    MusicMgr.GetInstance().PlaySound("Fail");
                }
            
                break;
        }
    }
}
