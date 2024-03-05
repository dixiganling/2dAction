using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private string name;
    // Start is called before the first frame update
    void Start()
    {
        name = this.transform.tag;
    }

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerG")
        {
            switch (name)
            {
                case "Grass":
                    MusicMgr.GetInstance().PlaySound("Player/Walk/Grass", false);
                    break;
                case "Rock":
                    MusicMgr.GetInstance().PlaySound("Player/Walk/Rock", false);
                    break;
                case "Wood":
                    MusicMgr.GetInstance().PlaySound("Player/Walk/Wood", false);
                    break;
            }
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        switch (name)
    //        {
    //            case "Grass":
    //                MusicMgr.GetInstance().PlaySound("Player/Walk/Grass", false);
    //                break;
    //            case "Rock":
    //                MusicMgr.GetInstance().PlaySound("Player/Walk/Rock", false);
    //                break;
    //            case "Wood":
    //                MusicMgr.GetInstance().PlaySound("Player/Walk/Wood", false);
    //                break;
    //        }
    //    }
    //}
}
