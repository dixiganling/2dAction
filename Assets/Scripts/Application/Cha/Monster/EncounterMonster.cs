using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum E_Level
{
    level1,
    level2,
    level3
}
public class EncounterMonster : MonoBehaviour
{
    public E_Level level;
    private EncounterModel enModel;
    private int id; 
    private EncounterProxy enProxy;

    //public Dictionary<int, Dictionary<string, int>> monsters = new Dictionary<int, Dictionary<string, int>>();

    public Transform[] monstersBorns;
    public Transform[] airWallsPos;
    // Start is called before the first frame update
    void Start()
    {
        //随机一个出怪组
        enProxy = GameFacade.GetInstance().RetrieveProxy(EncounterProxy.NAME) as EncounterProxy;
        enModel = enProxy.GetModel(level);

         //记录得到的出怪组
        EncounterModel model = new EncounterModel();
        for (int i = 0; i < monstersBorns.Length; i++)
        {
            model.monstersBorns.Add(monstersBorns[i].position);
        }
        foreach (string key in enModel.monsters.Keys)
        {
             model.name.Add(key);
        }
        model.id = enModel.id;
        model.WaveNum = enModel.WaveNum;
        //model.Ghost = enModel.Ghost;
        //model.GhostHalo = enModel.GhostHalo;
        //model.Wizard = enModel.Wizard;
        //model.Creeper = enModel.Creeper;
        model.monsters = enModel.monsters;
        model.nowWave = 0;
        model.nowMonster = 0;
        
        /*CreateMonstersModel model = new CreateMonstersModel();
        model.nowWave = 0;
        model.nowMonster = 0;
        for(int i = 0; i < monstersBorns.Length; i++)
        {
            model.monstersBorns.Add(monstersBorns[i].position);
        }*/
        //model.monstersBorns = monstersBorns;
        //model.waveNum = enModel.WaveNum;
        //model.ghostNum = enModel.Ghost;
        //model.ghostHaloNum = enModel.GhostHalo;

        /*for(int i = 0; i < waveNum; i++)
        {
            monsters[i].Add(PoolName.Ghost, ghostNum[i]);
            monsters[i].Add(PoolName.GhostHalo, ghostHaloNum[i]);
        }*/
        if (!GameFacade.GetInstance().HasProxy(CreateMonstersProxy.NAME))
        {
            GameFacade.GetInstance().RegisterProxy(new CreateMonstersProxy());
        }
      
       
            CreateMonstersProxy proxy = GameFacade.GetInstance().RetrieveProxy(CreateMonstersProxy.NAME) as CreateMonstersProxy;
        //记录所属的id
            id = proxy.AddMondel(model);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            //播放音乐
            MusicMgr.GetInstance().PlaySound("Encounter");
            UIManager.GetInstance().ShowPanel<TipPanel>("TipPanel");
            MusicMgr.GetInstance().PlayBkMusic("Monster");
            MusicMgr.GetInstance().PauseMainBKMusic();
            //生成怪物
            GameFacade.GetInstance().SendNotification(CommandName.CREATE_MONSTERS,id);
            //生成火墙
            GameFacade.GetInstance().SendNotification(CommandName.CREATE_FIREWALL, airWallsPos);
       

            Destroy(this.gameObject);


        }
    }
   
}
