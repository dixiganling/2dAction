using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPanel : BasePanel
{
    private Boss boss;
    private int id;
    public DialogSingleModel model;
    public override void ShowMe()
    {
        this.gameObject.SetActive(true);
        ChangeContent();
        boss = GameObject.Find("Cha/Monster/Boss").GetComponent<Boss>();
    }
    public override void HideMe()
    {
        this.gameObject.SetActive(false);
        id = 0;
        model = null;
    }
    protected override void OnClick(string btnName)
    {
        switch (btnName) {
            case "imgDia":
                //改变内容
                if (id < model.txt.Length)
                {
                    id++;

                    ChangeContent();
                }
                //开始战斗
                if (id == model.txt.Length)
                {
                    
                    MusicMgr.GetInstance().PlayBkMusic("Boss");
                    MusicMgr.GetInstance().PauseMainBKMusic();
                    //boss开始行动
                    boss.isStart = true;
                    //隐藏ui
                    UIManager.GetInstance().HidePanel("DialogPanel");
                    //显示血条
                    UIManager.GetInstance().ShowPanel<BossHpPanel>("BossHpPanel",E_UI_Layer.Top,(panel)=> {
                        panel.boss = boss;
                    });
                }
       
                break;
        }
    }
    public void ChangeContent()
    {
        if (id != model.txt.Length)
        {
            GetControl<Text>("txtDia").text = "  "+model.txt[id];
            GetControl<Image>("imgCha").sprite = ResMgr.GetInstance().Load<Sprite>("UI/Dialog/" + model.img[id]);
        }
          
    }
}
