using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleTextBlink : MonoBehaviour
{
    float timerSeconds = 0f;
    float duration = 0.5f;

    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update ()
    {
        timerSeconds += GameTime.deltaTime;
        if (timerSeconds >= duration)
        {
            timerSeconds = 0f;
            if (text.text.Contains("_"))
            {
                text.text = "Protagonist";
            }
            else
            {
                text.text = "Protagonist_";
            }
        }
	}
}
