using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        SceneManager.sceneLoaded += SceneLoadAction;
    }
   public void ChangeToMain()
    {
        SceneManager.LoadScene("Main");
    }
    
    public void SceneLoadAction(Scene scene,LoadSceneMode loadSceneMode)
    {
        Debug.Log("scene load=" + scene.name);

        if(scene.name=="Main")
        {
            MusicManager.instance.PlayMainMusic();
        }
        
    }
    
}
