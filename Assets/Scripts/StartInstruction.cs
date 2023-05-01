using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartInstruction : MonoBehaviour
{
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("Instruction");
    }
}