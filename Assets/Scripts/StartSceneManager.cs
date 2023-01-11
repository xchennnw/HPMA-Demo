using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartSceneManager : MonoBehaviour
{

    public Button button;


    void Start()
    {
        button.onClick.AddListener(() => buttonCallBack());
    }

    private void buttonCallBack()
    {        
         SceneManager.LoadScene("Battle");
    }
}
