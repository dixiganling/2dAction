using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPanelMediator : Mediator
{
    public static new string NAME = "RewardPanelMediator";
    public RewardPanelView panelView;
    public RewardProxy proxy;
    public List<int> has = new List<int>();
    public Player player;
    public RewardPanelMediator( object viewComponent = null) : base(NAME, viewComponent)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        panelView = viewComponent as RewardPanelView;
        proxy = Facade.RetrieveProxy(RewardProxy.NAME) as RewardProxy;
        //生成不同的奖励ui
        Init();
    }
    public void Init()
    {
        for (int i = 0; i < panelView.rewardCells.Length; i++)
        {
            //随机一个
            int j = StartRe();
            //初始化
            panelView.rewardCells[i].Init(proxy.GetReward(j));
            //记录已经生成的奖励
            has.Add(j);
        }
    }
    public int StartRe()
    {
        int j = Random.Range(0, proxy.DataLength);
        //判断技能相关
        ChaChong(ref j);
        //判断是否已经拥有
        ReRandom(ref j);
        return j;
    }
    public void ReRandom(ref int j)
    {
        for (int u = 0; u < has.Count; u++)
        {
            if (j == has[u])
            {
                j = Random.Range(0, proxy.DataLength);
                j = StartRe();
            }
        }

    }
    public override void OnRemove()
    {
        UIManager.GetInstance().HidePanel("RewardPanel");
    }
    public void ChaChong(ref int j)
    {

        //吸血相关
        if (proxy.GetReward(j).type == 3 )
        {
            //如果会吸血且再roll到吸血 或者不会吸血却roll到吸血相关
            if((player.playerModel.xiXue && proxy.GetReward(j).id == 5)|| (!player.playerModel.xiXue && proxy.GetReward(j).id != 5))
            {
                j = Random.Range(0, proxy.DataLength);
                ChaChong(ref j);
            }
 
        }
        //闪电相关
        if (proxy.GetReward(j).type == 2)
        {
            if ((player.playerModel.lighting && proxy.GetReward(j).id == 4) || (!player.playerModel.lighting && proxy.GetReward(j).id != 4))
            {
                j = Random.Range(0, proxy.DataLength);
                ChaChong(ref j);
            }
       
        }
        return;
    }
}
