using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandName : BaseManager<CommandName>
{
    //Scene名
    public const string START_SCENE = "1.Start";
    public const string COUNTRY_SCENE = "1.Country";
    public const string GAME_SCENE1_1 = "2.1";
    public const string GAME_SCENE1_2 = "2.2";
    public const string GAME_SCENE1_3 = "2.3";
    public const string GAME_SCENE1_4 = "2.4";
    public const string GAME_SCENE1_5 = "2.5";
    public const string GAME_SCENE1_6 = "2.6";
    public const string GAME_SCENE1_7 = "2.7";
    public const string GAME_SCENE1_8 = "2.8";
    public const string GAME_SCENE1_9 = "2.9";
    public const string GAME_SCENE1_10 = "2.10";
    //Controller
    public const string START_UP = "C_startup";
    public const string ENTER_SCENE = "C_enterscene";
    public const string EXIT_SCENE = "C_exitscene";
    public const string CREATE_MONSTERS = "C_createmonsters";
    public const string DELETE_AIRWALLS = "C_deleteairwalls";
    public const string GET_REWARD = "C_getreward";
    public const string SAVE_PLAYERMODEL = "C_saveplayermodel";
    public const string SHOW_SAVEPANEL = "C_showsavepanel";
    public const string CHANGE_NUM = "C_changenum";
    public const string CHANGE_DATA = "C_changedata";
    public const string SHOW_HAT = "C_showhat";
    public const string UPDATE_SAVEPANEL = "C_updatesavepanel";
    public const string START_WALL = "C_startwall";
    public const string CREATE_FIREWALL = "C_createfirewall";
}
