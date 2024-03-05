using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReusable
{
    //创建
    void OnSpawn();
    //回收
    void OnUnSpawn();
}

