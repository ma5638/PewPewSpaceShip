using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingBoard : MonoBehaviour
{
    [SerializeField] GameObject settingBoard;
    private GameObject startButton;
    private GameObject instructionButton;
    private GameObject settingButton;
    
    private void Awake()
    {
        startButton = GameObject.Find("Start");
        settingButton = GameObject.Find("Setting");
        instructionButton = GameObject.Find("Instruction");
    }

    public void Setting()
    {
        settingBoard.SetActive(true);
        startButton.SetActive(false);
        settingButton.SetActive(false);
        instructionButton.SetActive(false);
    }

    public void BackButton()
    {
        settingBoard.SetActive(false);
        startButton.SetActive(true);
        settingButton.SetActive(true);
        instructionButton.SetActive(true);
    }
}