using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonstersModel
{
    public int waveNum;
    public int nowWave;
    public int nowMonster;

    //public Transform[] monstersBorns;
    public List<Vector3> monstersBorns = new List<Vector3>();
    public List<int> ghostNum;
    public List<int> ghostHaloNum;
}
