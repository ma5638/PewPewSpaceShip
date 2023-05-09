using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("score"));
        gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("score").ToString();
    }
}
