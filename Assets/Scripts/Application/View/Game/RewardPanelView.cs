using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPanelView : BasePanel
{
    public RewardCell[] rewardCells;
    public override void ShowMe()
    {
        this.gameObject.SetActive(true);
    }
    public override void HideMe()
    {
        this.gameObject.SetActive(false);
    }
}
