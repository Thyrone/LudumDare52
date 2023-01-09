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

    public static int highscore = 0;


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
        PlayerController.scoreEvent += ScoreUpdate;
        if (PlayerPrefs.HasKey("Highscore"))
        {
            PlayerPrefs.GetInt("Highscore", highscore);
        }
        else
        {
            PlayerPrefs.SetInt("Highscore", 0);
        }
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

            UIManager.instance.ChangeBanWhiteIcon(pickObjects.Find((x) => x.objectType == WhiteObject).icon, pickObjects.Find((x) => x.objectType == BanObject).icon);
        }

        if (MusicManager.lastMarkString.Contains("End"))
        {
            Dead();
        }


        }
    private void OnDestroy()
    {
        MusicManager.beatUpdated -= ChangeRules;
        PlayerController.hurtEvent -= TakeDamage;
        PlayerController.scoreEvent -= ScoreUpdate;
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

    void ScoreUpdate(int scoreToUpdate)
    {
        score += scoreToUpdate;
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", score);
        }
        uiManager.ScoreUIUpdate();
    }
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage();
        }
    }

}
