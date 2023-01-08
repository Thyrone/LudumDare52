using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public delegate void HurtDelegate();
    public static event HurtDelegate hurtEvent;

    // Start is called before the first frame update
    void Start()
    {
        hurtEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ObjectController>().objectType==GameManager.BanObject)
        {
            hurtEvent();
        }
    }
}
