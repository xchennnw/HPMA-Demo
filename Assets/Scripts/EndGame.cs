using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public string winOrLose;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
