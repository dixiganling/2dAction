using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    float alpha;
    bool show ;
    public override void ShowMe()
    {
        this.gameObject.SetActive(true);
        alpha = 0;
        show = false;
    }
    public override void HideMe()
    {
        this.gameObject.SetActive(false);
      
    }
    private void Update()
    {
        if (!show)
        {
            alpha += Time.deltaTime * 2;
            GetControl<Text>("txt").color = new Color(1, 0, 0, alpha);
            if (alpha >= 1)
                show = true;
        }
        else
        {
            alpha -= Time.deltaTime * 2;
            GetControl<Text>("txt").color = new Color(1, 0, 0, alpha);
            if (alpha <= 0)
                UIManager.GetInstance().HidePanel("TipPanel");
        }
    
    }
}
