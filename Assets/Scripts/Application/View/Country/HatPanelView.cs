using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatPanelView : BasePanel
{
    public HatCell[] hatCells;
    public override void HideMe()
    {
        this.gameObject.SetActive(false);
    }
    public override void ShowMe()
    {
        this.gameObject.SetActive(true);
    }
    public void Change(string txt)
    {
        GetControl<Text>("txtSoul").text = txt;
    }
}
