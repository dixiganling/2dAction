using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePanel : BasePanel
{
    public SaveCell[] savePanels;
    public override void ShowMe()
    {
        this.gameObject.SetActive(true);
    }
    public override void HideMe()
    {
        this.gameObject.SetActive(false);
    }
}
