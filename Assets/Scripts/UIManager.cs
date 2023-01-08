using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image whiteUI;
    public Image banUI;
    public Image[] emptyHeart;
    public Image[] heart;



    public GameObject GameOverUI;
    public TMP_Text ScoreTxt;

    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void ChangeBanWhiteIcon(Sprite goodSprite,Sprite badSprite)
    {
        whiteUI.sprite = goodSprite;
        banUI.sprite = badSprite;
    }

    public void GameOver()
    {
        GameOverUI.SetActive(true);
    }

    public void LoseHeart(int heartToDisapear)
    {
        heart[heartToDisapear-1].gameObject.SetActive(false);
        emptyHeart[heartToDisapear-1].gameObject.SetActive(true);
    }
    void Start()
    {
        GameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ScoreTxt.text = GameManager.score.ToString();
    }
}
