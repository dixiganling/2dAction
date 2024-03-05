using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterModel
{
    public int id;
    public int WaveNum;
    //public List<int> Ghost;
    //public List<int> GhostHalo;
    //public List<int> Wizard;
    //public List<int> Creeper;
    public int nowWave;
    public int nowMonster;
    public Dictionary<string, List<int>> monsters;
    public List<string> name = new List<string>();
    public List<Vector3> monstersBorns = new List<Vector3>();

}
