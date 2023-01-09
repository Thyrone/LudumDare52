using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int score;
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
        if(MusicManager.lastMarkString.Contains("ChangeRules"))
        {
            Debug.Log("ChangeRules");
            WhiteObject = (ObjectType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(ObjectType)).Length);
            do
            {
                BanObject = (ObjectType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(ObjectType)).Length);
            }
            while (BanObject == WhiteObject);

            uiManager.ChangeBanWhiteIcon(pickObjects.Find((x) => x.objectType == WhiteObject).icon, pickObjects.Find((x) => x.objectType == BanObject).icon);
        }
       
      
    }
    private void OnDestroy()
    {
        MusicManager.beatUpdated -= ChangeRules;
        PlayerController.hurtEvent -= TakeDamage;
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
        
        if(Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage();
        }
    }

}
