using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimBeat : MonoBehaviour
{
    public Sprite idle1;
    public Sprite idle2;

    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        MusicManager.beatUpdated += ChangeSprite;
    }

    void ChangeSprite()
    {

        if (image.sprite == idle1)
        {
            image.sprite = idle2;
            Debug.Log("idle2");
        }
        else if (image.sprite == idle2)
        {
            image.sprite = idle1;
            Debug.Log("idle1");
        }
    }
}
