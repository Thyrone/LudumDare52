using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBeat : MonoBehaviour
{
    public Sprite idle1;
    public Sprite idle2;

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        MusicManager.beatUpdated += ChangeSprite;
    }

    void ChangeSprite()
    {
        Debug.Log("ChangeSprite" + GetComponent<SpriteRenderer>().sprite);

        if (spriteRenderer.sprite == idle1)
        {
            spriteRenderer.sprite = idle2;
            Debug.Log("idle2");
        }
        else if (spriteRenderer.sprite == idle2)
        {
            spriteRenderer.sprite = idle1;
            Debug.Log("idle1");
        }
    }
}
