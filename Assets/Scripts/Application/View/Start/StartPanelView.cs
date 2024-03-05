using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanelView : BasePanel
{
    public override void HideMe()
    {
        this.gameObject.SetActive(false);
    }

    public override void ShowMe()
    {
        this.gameObject.SetActive(true);
    }
    
}
