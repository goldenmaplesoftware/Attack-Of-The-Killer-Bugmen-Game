using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceenSwitch : MonoBehaviour
{
    public static string previousScene;
    public static string currentScene;
    public virtual void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void SwitchScene(string sceneName) 
    {
        previousScene = currentScene;
        SceneManager.LoadScene(sceneName);
    
    }



}
