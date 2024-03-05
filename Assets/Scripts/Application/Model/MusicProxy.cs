using PureMVC.Patterns.Proxy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicProxy : Proxy
{
    public new static string NAME = "MusicProxy";
    private MusicModel model;
    public MusicProxy( object data = null) : base(NAME, data)
    {
        model = JsonMgr.Instance.LoadData<MusicModel>("musicModel");
        MusicMgr.GetInstance().ChangeBKValue(model.bkValue);
        MusicMgr.GetInstance().ChangeSoundValue(model.effValue);
    }
    public MusicModel GetMusicModel()
    {
        return model;
    }
    public void SaveData()
    {
        JsonMgr.Instance.SaveData(model, "musicModel");
    }
}
