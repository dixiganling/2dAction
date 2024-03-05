using PureMVC.Patterns.Proxy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelProxy : Proxy
{
    public new static string NAME = "PlayerModelProxy";
    private PlayerModel playerModel;
    private SaveModelProxy save;
    private List<HatModel> nowAdd;
    private MusicProxy musicProxy;
    public PlayerModelProxy(object data = null) : base(NAME, data)
    {
        playerModel = new PlayerModel();
        musicProxy = Facade.RetrieveProxy(MusicProxy.NAME) as MusicProxy;
        playerModel.control = musicProxy.GetMusicModel().control;
        save = Facade.RetrieveProxy(SaveModelProxy.NAME) as SaveModelProxy;
        nowAdd = save.ReturnModel(save.NowId).hatModels;
        InitAdd();
    }
    /// <summary>
    /// 加上增益效果
    /// </summary>
    public void InitAdd()
    {
        //最大生命值
        playerModel.maxHp += nowAdd[0].singleValue * nowAdd[0].nowLevel;
        //最大精力
        playerModel.maxEnergy += nowAdd[1].singleValue * nowAdd[1].nowLevel;
        //精力恢复速度
        playerModel.addEnergySpeed += nowAdd[2].singleValue * nowAdd[2].nowLevel;

        //重置
        playerModel.hp = playerModel.maxHp;
        playerModel.energy = playerModel.maxEnergy;
    }
    public PlayerModel ReturnModel()
    {
        return playerModel;
    }
}
