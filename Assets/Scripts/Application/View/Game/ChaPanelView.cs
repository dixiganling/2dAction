using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaPanelView : BasePanel
{
    public Player player;
    RectTransform hp;
    RectTransform en;

    // Start is called before the first frame update
    void Start()
    {
        hp = GetControl<Slider>("Hp").GetComponent<RectTransform>();
        en = GetControl<Slider>("Energy").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHp();
        GetControl<Slider>("Energy").value = player.Energy/player.MaxEnergy;
    }
    public void ChangeHp()
    {
        GetControl<Slider>("Hp").value = player.Hp / player.MaxHp;
    }
    public override void HideMe()
    {
        this.gameObject.SetActive(false);
    }

    public override void ShowMe()
    {
        this.gameObject.SetActive(true);
    }
    public void ChangeHpWidth(float add)
    {
        hp.sizeDelta = new Vector2(300f*add,hp.sizeDelta.y);
    }
    public void ChangeEneWidth(float add)
    {
        en.sizeDelta = new Vector2(300f*add, en.sizeDelta.y);
    }
}
