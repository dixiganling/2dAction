using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum E_Gate {
    gate1,
    gate2,
    gate3
}

public class Gate : MonoBehaviour
{
    private ControlPanel controlPanel;
    public E_Gate e_Gate;
    public int id;
    private string name;
    private void Start()
    {
        GetSceneName();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(controlPanel == null)
        controlPanel = UIManager.GetInstance().GetPanel<ControlPanel>("ControlPanel");
        if (collision.tag == "Player")
        {
            //显示进入关卡按钮
            controlPanel.btnEnter.gameObject.SetActive(true);
            controlPanel.enterSceneName = name;
            controlPanel.name = "gate";
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
    public void GetSceneName()
    {
        int key = 3;
        switch (id)
        {
            case 0:
                key = Random.Range(1, 4);
                //key = 2;
                break;
            case 1:
                key = Random.Range(4, 7);
                break;
            case 2:
                key = Random.Range(7, 10);
                break;
            case 3:
                key = 10;
                break;
            case 4:
                key = -1;
                break;
        }
        switch (e_Gate)
        {
            case E_Gate.gate1:
                SelectGate(key,"2.");
                break;
            case E_Gate.gate2:
                SelectGate(key, "3.");
                break;
            case E_Gate.gate3:
                SelectGate(key, "4.");
                break;
        }

    }
    public void SelectGate(int key,string level)
    {
        switch (key)
        {
            case 1:
                name = CommandName.GAME_SCENE1_1.Split('.')[1];
                break;
            case 2:
                name = CommandName.GAME_SCENE1_2.Split('.')[1];
                break;
            case 3:
                name = CommandName.GAME_SCENE1_3.Split('.')[1];
                break;
            case 4:
                name = CommandName.GAME_SCENE1_4.Split('.')[1];
                break;
            case 5:
                name = CommandName.GAME_SCENE1_5.Split('.')[1];
                break;
            case 6:
                name = CommandName.GAME_SCENE1_6.Split('.')[1];
                break;
            case 7:
                name = CommandName.GAME_SCENE1_7.Split('.')[1];
                break;
            case 8:
                name = CommandName.GAME_SCENE1_8.Split('.')[1];
                break;
            case 9:
                name = CommandName.GAME_SCENE1_9.Split('.')[1];
                break;
            case 10:
                name = CommandName.GAME_SCENE1_10.Split('.')[1];
                break;
            default:
                name = CommandName.COUNTRY_SCENE;
                return;
        }
        name = level + name;
    }
}
