using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndSceneScript : MonoBehaviour
{
    string txt;
    public TextMeshProUGUI endText;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        txt = GameObject.Find("EndGameManager").GetComponent<EndGame>().winOrLose;
        endText.text = txt;
    }

    void OnEnable()
    {
        //Register Button Events
        button.onClick.AddListener(() => buttonCallBack(button));

    }

    private void buttonCallBack(Button buttonPressed)
    {
        if (buttonPressed == button)
        {
            SceneManager.LoadScene("StartScene");
        }

    }
}
