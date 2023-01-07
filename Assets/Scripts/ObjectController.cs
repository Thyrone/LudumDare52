using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.deltaTime);
        if(MusicManager.lastMarkString== "Marker A")
        {
            transform.position -= new Vector3(0f, (MusicManager.lastTempo/60) * Time.deltaTime, 0f);
        }
        
    }
}
