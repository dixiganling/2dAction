using PureMVC.Patterns.Proxy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterProxy : Proxy
{   
    public new static string NAME = "EncounterProxy";
    private List<EncounterModel> encounterModels;
    int id;
    public EncounterProxy( object data = null) : base(NAME, data)
    {
        encounterModels = JsonMgr.Instance.LoadData<List<EncounterModel>>("Json/EncounterInfo");
    }
    public EncounterModel GetModel(E_Level level)
    {
        switch (level)
        {
            case E_Level.level1:
                //id = 0;
                id = Random.Range(1, 5);
                break;
            case E_Level.level2:
                id = Random.Range( 5,9);
                break;
            case E_Level.level3:
                id = Random.Range(9, 13);
                break;
        }
        //Name(id);
        Debug.Log(id);

        return encounterModels[id];
    }
    //public void Name(int id)
    //{
        
    //    foreach (string key in encounterModels[id].monsters.Keys)
    //    {
    //        encounterModels[id].name.Add(key);
    //    }
   
    //}
}
