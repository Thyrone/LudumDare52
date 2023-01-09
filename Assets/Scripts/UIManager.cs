using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] whiteUI;
    public Image[] banUI;
    public Image[] emptyHeart;
    public Image[] heart;



    public GameObject GameOverUI;
    public GameObject ChangeRulesUI;
    public TMP_Text[] ScoreTxt;
    public TMP_Text HighScoreTxt;

    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void ChangeBanWhiteIcon(Sprite goodSprite,Sprite badSprite)
    {
        foreach(Image image in whiteUI)
        {
            image.sprite = goodSprite;
        }
        foreach (Image image in banUI)
        {
            image.sprite = badSprite;
        }
        ChangeRulesUI.GetComponentInChildren<Animator>().Play("BubbleAnim");
        ChangeRulesUI.SetActive(true);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverUITime());
    }

    public void ScoreUIUpdate()
    {
        foreach(TMP_Text tmp_text in ScoreTxt)
        {
            tmp_text.text = GameManager.score.ToString();

            if (tmp_text.gameObject.TryGetComponent(out Animator animator))
            {
                Debug.Log(animator.gameObject.name);
                animator.SetTrigger("score");
            }
        }
        
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


    IEnumerator GameOverUITime()
    {
        yield return new WaitForSeconds(1.2f);
        HighScoreTxt.text = GameManager.highscore.ToString();
        GameOverUI.SetActive(true);
        
    }
}
