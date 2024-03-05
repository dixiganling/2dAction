using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpPanel : BasePanel
{
    public Boss boss;
    public override void HideMe()
    {
        this.gameObject.SetActive(false);
    }

    public override void ShowMe()
    {
        this.gameObject.SetActive(true);
    }
    private void Update()
    {
        GetControl<Slider>("Hp").value = (float)boss.HP / boss.maxHp;
 
    }
}
