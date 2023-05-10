using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    private static BackgroundMusic backgroundMusic;

    void Awake()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log(currentSceneName);
        if (backgroundMusic == null && currentSceneName != "End")
        {
            backgroundMusic = this;
            DontDestroyOnLoad(backgroundMusic);
        }
        else if (currentSceneName == "End"){
            Destroy(GameObject.Find("Background music"));
            backgroundMusic = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    // void Awake()
    // {
    //     string currentSceneName = SceneManager.GetActiveScene().name;
    //     Debug.Log(currentSceneName);
    //     if (backgroundMusic == null)
    //     {
    //         backgroundMusic = this;
    //         // DontDestroyOnLoad(backgroundMusic);
    //     // }
    //     // else if (currentSceneName == "End"){
    //     //     backgroundMusic = this;
    //     }
    //     else {
    //         Destroy(gameObject);
    //     }
    // }

    // Update is called once per frame
}
