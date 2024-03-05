using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveCell : BasePanel
{
    public int id;
    public SaveModel saveModel;
    public Image load;
    public bool isXin;
    //List<object> obj = new List<object>();
    public override void ShowMe()
    {
        this.gameObject.SetActive(true);

    }
    public override void HideMe()
    {
        this.gameObject.SetActive(false);

    }
    protected override void Awake()
    {
        base.Awake();
        load = GetControl<Image>("txtBk");

    }
    void Start()
    {
        //退出的场景
        //obj.Add(SceneManager.GetActiveScene().name);
        //obj.Add(saveModel);
        Load();

    }
    public void Load()
    {
        if (saveModel != null)
            Init(saveModel.oldSoul.ToString(), saveModel.time);
    }
    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            //存储对应的信息
            case "Bk":
                    UIManager.GetInstance().ShowPanel<SureSavePanel>("SureSavePanel", E_UI_Layer.System, (obj) => {
                        obj.id = this.id;
                        obj.isXin = this.isXin;
                    });
                
   
                    break;
            //加载对应的场景
            case "txtBk":
                GameFacade.GetInstance().SendNotification(CommandName.ENTER_SCENE, id, CommandName.COUNTRY_SCENE);
                break;
        }
    }
    public void Init(string text,string time)
    {
        GetControl<Text>("Text").text = " 灵魂：" + text +" 时间："+time;
    }
}
