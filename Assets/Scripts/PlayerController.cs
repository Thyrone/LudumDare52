using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public delegate void HurtDelegate();
    public static event HurtDelegate hurtEvent;

    ObjectController objectUnder;

    Animator playerAnimator;

    void Start()
    {
        playerAnimator=GetComponentInChildren<Animator>();
        hurtEvent();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimator.SetTrigger("Down");

            if(objectUnder != null)
            {
                if (objectUnder.objectType == GameManager.BanObject)
                {
                    hurtEvent();
                }
                else if (objectUnder.objectType == GameManager.WhiteObject)
                {
                    GameManager.score += 50;
                }
                else
                {
                    GameManager.score += 10;
                }
                Destroy(objectUnder);
            }
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
            if (other.TryGetComponent(out ObjectController objectController))
            {
                objectUnder = objectController;
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ObjectController objectController))
        {
            if(objectController == objectUnder)
                objectUnder = null;
        }
    }

}
