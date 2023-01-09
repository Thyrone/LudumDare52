using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public delegate void HurtDelegate();
    public static event HurtDelegate hurtEvent;

    ObjectController objectUnder;

    Animator playerAnimator;

    public ParticleSystem particleSystem;
    void Start()
    {
        playerAnimator=GetComponentInChildren<Animator>();
        
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
                    particleSystem.startColor = Color.red;
                    particleSystem.Play();
                    hurtEvent();
                }
                else if (objectUnder.objectType == GameManager.WhiteObject)
                {
                    GameManager.score += 50;
                    particleSystem.startColor = Color.green;
                    particleSystem.Play();
                }
                else
                {
                    GameManager.score += 10;
                    particleSystem.startColor = Color.white;
                    particleSystem.Play();
                }
                Destroy(objectUnder.gameObject);
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
