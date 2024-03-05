using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    PlayerModel model = new PlayerModel();
    // Start is called before the first frame update
    void Start()
    {
        print(Application.persistentDataPath);
        GameFacade.GetInstance().Launch();

    }

    
}
