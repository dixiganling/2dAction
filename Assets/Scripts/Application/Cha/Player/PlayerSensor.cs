using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    private int m_ColCount = 0;
    private float m_DisableTimer;
    private void OnEnable()
    {
        m_ColCount = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.tag != "AirWall"&&collision.tag != "FireWall")
        
        if(collision.gameObject.layer == 10)
        m_ColCount++;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.tag != "AirWall" && collision.tag != "FireWall")
        if (collision.gameObject.layer == 10)
            m_ColCount--;
    }
    public bool State()
    {
        //刚起跳就返回false
        if (m_DisableTimer > 0)
            return false;
        //与地板接触返回true，从地板上下落就返回false
        return m_ColCount > 0;
    }
    // Update is called once per frame
    void Update()
    {
        m_DisableTimer -= Time.deltaTime;
    }
    //刚起跳时调用
    public void Disable(float duration)
    {
        m_DisableTimer = duration;
    }
}
