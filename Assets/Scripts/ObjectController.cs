using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType { Guy, Cow, Car };

public class ObjectController : MonoBehaviour
{
    public Sprite idle1;
    public Sprite idle2;

    public ObjectType objectType;

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
        MusicManager.beatUpdated += ChangeSprite;
    }

    void ChangeSprite()
    {
        Debug.Log("ChangeSprite"+ GetComponent<SpriteRenderer>().sprite);

        if (spriteRenderer.sprite == idle1)
        {
            spriteRenderer.sprite = idle2;
            Debug.Log("idle2");
        }else if (spriteRenderer.sprite == idle2)
        {
            spriteRenderer.sprite = idle1;
            Debug.Log("idle1");
        }
            


    }

    void Update()
    {
        //Debug.Log(Time.deltaTime);
        if(MusicManager.lastMarkString== "Marker A")
        {
          //  transform.position -= new Vector3(0f, (MusicManager.lastTempo/60) * Time.deltaTime, 0f);
        }
        
    }
}
