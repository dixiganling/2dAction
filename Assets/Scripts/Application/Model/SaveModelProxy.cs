using PureMVC.Patterns.Proxy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveModelProxy : Proxy
{
    public int NowId
    {
        get;
        set;
    }
    public new static string NAME = "SaveModelProxy";
    private Dictionary<string,SaveModel> saveModels;
    public SaveModelProxy(object data = null) : base(NAME, data)
    {
        saveModels = JsonMgr.Instance.LoadData<Dictionary<string, SaveModel>>("saveModels");
    }
    /// <summary>
    /// 记录存档
    /// </summary>
    /// <param name="i"></param>
    /// <param name="saveModel"></param>
    public void AddSaveModel(int i,SaveModel saveModel)
    {
        string key = "model" + i.ToString();
        saveModels[key] = saveModel;
    }
    /// <summary>
    /// 修改灵魂数量
    /// </summary>
    /// <param name="i"></param>
    /// <param name="num"></param>
    public void ChangeSoul(int num)
    {
        //修改数据
        string key = "model" + NowId.ToString();
        saveModels[key].soul += num;
        //修改ui
        Facade.SendNotification(CommandName.CHANGE_NUM);
    }
    public int GetSaveModelsLength()
    {
        return saveModels.Count;
    }
    /// <summary>
    /// 返回存档
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public SaveModel ReturnModel(int i)
    {
        string key = "model" + i.ToString();
        if (saveModels.ContainsKey(key))
            return saveModels[key];
        else
            return null;
    }
    /// <summary>
    /// 存储存档
    /// </summary>
    public void Save()
    {
        JsonMgr.Instance.SaveData(saveModels, "saveModels");
    }
}
