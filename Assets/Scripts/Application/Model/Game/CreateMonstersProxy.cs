using PureMVC.Patterns.Proxy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonstersProxy : Proxy
{
    public new static string NAME = "CreateMonstersProxy";
    //public CreateMonstersModel[] monstersModel = new CreateMonstersModel[100];
    public List<EncounterModel> monstersModels = new List<EncounterModel>();
    public CreateMonstersProxy(object data = null) : base(NAME, data)
    {
        //monstersModel[id] = data as CreateMonstersModel;
        //monstersModels.Add(data as CreateMonstersModel);

    }
    public int AddMondel(EncounterModel model)
    {
        
        monstersModels.Add(model);
        int index = monstersModels.IndexOf(model);
        //Debug.Log(index);
        return index;
        //monstersModel[id] = model;
    }
    public void AddMonster(EncounterModel model)
    {
        model.nowMonster++;
    }
    public void ReduceMonster(EncounterModel model)
    {
        int index = monstersModels.IndexOf(model);
        model.nowMonster--;
        if (model.nowMonster <= 0)
        {
            //判断是否生成第二波
            if (model.nowWave < model.WaveNum)
            {
                SendNotification(CommandName.CREATE_MONSTERS,index);
            }
            else
            {
                //去掉空气墙
                SendNotification(CommandName.DELETE_AIRWALLS);
                //让该数据置空
                //monstersModels.Remove(model);
            }
        }
    }
    public void ReduceMonster(int id)
    {
        monstersModels[id].nowMonster--;
        if (monstersModels[id].nowMonster <= 0)
        {
            //判断是否生成第二波
            if (monstersModels[id].nowWave < monstersModels[id].WaveNum)
            {
                SendNotification(CommandName.CREATE_MONSTERS,id);
            }
            else
            {
                //去掉空气墙
                SendNotification(CommandName.DELETE_AIRWALLS);
                //让该数据置空
                //monstersModels.RemoveAt(id);
            }
        }
    }
    public void AddWave(EncounterModel model)
    {
        model.nowWave++;
    }
    
}
