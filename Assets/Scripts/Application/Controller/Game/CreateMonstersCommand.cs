using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonstersCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        CreateMonstersProxy proxy = Facade.RetrieveProxy(CreateMonstersProxy.NAME) as CreateMonstersProxy;
        MonoMgr.GetInstance().StartCoroutine(CreateMonster(proxy, (int)notification.Body));
    }

    //生成怪物
    public IEnumerator CreateMonster(CreateMonstersProxy proxy,int id)
    {

        EncounterModel model = proxy.monstersModels[id];
        if (model.nowWave < model.WaveNum)
         {
            Debug.Log(model.nowWave);
            Debug.Log(model.WaveNum);
            //记录

            for(int i = 0; i< model.name.Count; i++)
            {
             
                    for (int k = 0; k < model.monsters[model.name[i]][model.nowWave]; k++)
                    {
                      
                        GetObj(model.name[i], model, proxy, id);
                        yield return new WaitForSeconds(1f);
                    }
                
            }
            proxy.AddWave(model);
            //Ghost
            /* if (model.Ghost.Count != 0)
             {
                 for (int i = 0; i < model.Ghost[model.nowWave]; i++)
                 {

                     GetObj(PoolName.Ghost, model, proxy, id);
                     yield return new WaitForSeconds(1f);
                 }
             }
             //GhostHalo
             if (model.GhostHalo.Count != 0)
             {

                 for (int i = 0; i < model.GhostHalo[model.nowWave]; i++)
                 {

                     GetObj(PoolName.GhostHalo, model, proxy, id, false);

                     yield return new WaitForSeconds(1f);
                 }
             }
             //Wizard
             if (model.Wizard.Count != 0)
             {

                 for (int i = 0; i < model.Wizard[model.nowWave]; i++)
                 {

                     GetObj(PoolName.Wizard, model, proxy, id);

                     yield return new WaitForSeconds(1f);
                 }
             }
             //Creeper
             if (model.Creeper.Count != 0)
             {

                 for (int i = 0; i < model.Creeper[model.nowWave]; i++)
                 {

                     GetObj(PoolName.Creeper, model, proxy, id);

                     yield return new WaitForSeconds(1f);
                 }
             }*/

        }
 

    }
    private void GetObj(string name, EncounterModel model, CreateMonstersProxy proxy, int id)
    {
        int pos;
        
           
        if (name == PoolName.Angle || name == PoolName.GhostHalo)
            pos = Random.Range(0, model.monstersBorns.Count);
        else
            pos = Random.Range(0, 2);
        PoolMgr.GetInstance().GetObj(name, (obj) => {
            obj.transform.position = model.monstersBorns[pos];
            obj.GetComponent<Monster>().id = id;
        });
        proxy.AddMonster(model);

    }


}

