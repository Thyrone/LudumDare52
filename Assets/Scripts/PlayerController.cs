using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public delegate void HurtDelegate();
    public static event HurtDelegate hurtEvent;

    public delegate void ScoreDelegate(int addScore);
    public static event ScoreDelegate scoreEvent;

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
                    MusicManager.instance.PlayError();
                }
                else if (objectUnder.objectType == GameManager.WhiteObject)
                {
                   // GameManager.score += 50;
                    scoreEvent(50);
                    particleSystem.startColor = Color.green;
                    particleSystem.Play();
                    MusicManager.instance.PlayParticleSnd();
                }
                else
                {
                    MusicManager.instance.PlayParticleSnd();
                    scoreEvent(10);
                   // GameManager.score += 10;
                    particleSystem.startColor = Color.white;
                    particleSystem.Play();

                    switch(objectUnder.objectType)
                    {
                        case ObjectType.Car:
                            MusicManager.instance.PlayCar();
                            break;

                        case ObjectType.Cow:
                            MusicManager.instance.PlayCow();
                            break;

                        case ObjectType.Guy:
                            MusicManager.instance.PlayGuy();
                            break;

                        case ObjectType.Tree:
                            MusicManager.instance.PlayTree();
                            break;

                        case ObjectType.Watermelon:
                            MusicManager.instance.PlayWatermelon();
                            break;

                    }
                        

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
