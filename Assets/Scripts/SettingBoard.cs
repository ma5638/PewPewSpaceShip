using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingBoard : MonoBehaviour
{
    [SerializeField] GameObject settingBoard;

    public void Music(){
        SceneManager.LoadScene("GameScene");
    }
}
