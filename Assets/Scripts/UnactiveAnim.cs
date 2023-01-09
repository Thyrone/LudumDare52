using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnactiveAnim : MonoBehaviour
{
   public GameObject gameObjectToUnactive;
   public void Unactive()
    {
        gameObjectToUnactive.SetActive(false);
    }
}
