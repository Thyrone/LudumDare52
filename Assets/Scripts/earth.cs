using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earth : MonoBehaviour
{
    bool gameStart=false;
    // Update is called once per frame
    void Update()
    {
        if (MusicManager.lastMarkString == "Start")
            gameStart = true;
           
        if(gameStart)
            transform.Rotate(new Vector3(0,0,10) * ((MusicManager.lastTempo / 60) * Time.deltaTime));
    }
}
