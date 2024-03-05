using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogPanel1 : BasePanel
{
    public bool isBoss; 
    public DialogSingleModel model;
    private int id;
    public override void ShowMe()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(ChangeContent());
    }
    public override void HideMe()
    {
        this.gameObject.SetActive(false);
        id = 0;
        model = null;
    }
    IEnumerator ChangeContent()
    {
        while (id != model.txt.Length)
        {
            //决定颜色
            Color color;
            string str = model.img[id];
            if (str == "Boss")
                color = Color.red;
            else
                color = Color.white;
            GetControl<Text>("txtDia").text = "  " + model.txt[id];
            GetControl<Text>("txtDia").color = color;
            yield return new WaitForSeconds(1);
            id++;
        }
        //删除面板
        UIManager.GetInstance().HidePanel("dialogPanel1");
        if (isBoss)
        {
            //死亡
            EventCenter.GetInstance().EventTrigger("BossDead");
        }
      

    }
}
