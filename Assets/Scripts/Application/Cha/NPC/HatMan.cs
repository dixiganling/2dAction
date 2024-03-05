using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatMan : MonoBehaviour
{
    private ControlPanel controlPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (controlPanel == null)
            controlPanel = UIManager.GetInstance().GetPanel<ControlPanel>("ControlPanel");
        if (collision.tag == "Player")
        {
            //播放音效
            MusicMgr.GetInstance().PlaySound("Hat/Open" + Random.Range(1, 5).ToString(),false);
            //显示进入关卡按钮
            controlPanel.btnEnter.gameObject.SetActive(true);
            controlPanel.enterSceneName = name;
            controlPanel.name = "hatman";
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //隐藏进入关卡按钮
            controlPanel.btnEnter.gameObject.SetActive(false);
        }
    }
}
