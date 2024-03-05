using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardCell : BasePanel
{
    private RewardModel model;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Init(RewardModel model)
    {
        this.model = model;
        GetControl<Image>("Image").sprite = ResMgr.GetInstance().Load<Sprite>("UI/Reward/"+model.img);
        GetControl<Text>("Text").text = model.tips;
    }
    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "Image":
                GameFacade.GetInstance().SendNotification(CommandName.GET_REWARD,model.id);
                //播放背景音乐
                MusicMgr.GetInstance().UnpauseMainBKMusic();
                Time.timeScale = 1;
                break;
        }
    }
}
