using PureMVC.Patterns.Proxy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardProxy : Proxy
{
    private List<RewardModel> data;
    public new static string NAME = "RewardProxy";
    public int DataLength
    {
        get
        {
            return data.Count;
        }
    }
    public RewardProxy( object data = null) : base(NAME, data)
    {
        this.data = JsonMgr.Instance.LoadData<List<RewardModel>>("Json/RewardInfo");
    }
    public RewardModel GetReward(int i)
    {
        return data[i];
    }
    //public void RemoveReward(int i)
    //{
    //    data.RemoveAt(i);
    //}

}
