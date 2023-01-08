using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    int score;
    public int maxLife = 3;
    int currentLife;
    public static ObjectType WhiteObject;
    public static ObjectType BanObject;

    public List<PickObject> pickObjects = new List<PickObject>();

    UIManager uiManager;


    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife; 
        uiManager = UIManager.instance;
        MusicManager.markerUpdated += ChangeRules;
        PlayerController.hurtEvent += TakeDamage;
    }

    void ChangeRules()
    {
        WhiteObject= (ObjectType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(ObjectType)).Length);
        do
        {
            BanObject = (ObjectType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(ObjectType)).Length);
        }
        while (BanObject == WhiteObject);

        uiManager.ChangeGoodIcon(pickObjects.Find((x) => x.objectType == WhiteObject).icon);
        uiManager.ChangeBadIcon(pickObjects.Find((x) => x.objectType == BanObject).icon);
      
    }

   void Dead()
    {
        uiManager.GameOver();
    }

    void TakeDamage()
    {
        uiManager.LoseHeart(currentLife);
        currentLife--;
        if (currentLife == 0)
        {
            Dead();
        }
    }
    void Update()
    {
        if(currentLife==0)
        {
            Dead();
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage();
        }
    }

}
