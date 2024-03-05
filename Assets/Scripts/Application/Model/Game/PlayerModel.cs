using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel 
{
    public float speed = 4;
    public int atk=5;
    public float maxHp = 100;
    public float hp=100;
    public float maxEnergy = 100;
    public float energy=100;
    public float addEnergySpeed = 10;
    public E_Control control = E_Control.phone;
    //吸血效果
    public int xiXueValue = 1;
    public bool xiXue;
    //闪电效果
    public bool lighting;
    public int lightingNum = 1;
    public int lightingHurt = 8;
}
