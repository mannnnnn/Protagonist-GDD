using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Outputs to DialogUserInput to freeze dialog for some duration
 */
public class DialogFreezer : MonoBehaviour
{
    DialogUserInput userInput;

    float duration;
    float timerSeconds = 0f;
    public void Initialize(float duration)
    {
        this.duration = duration;
    }

    void Start()
    {
        userInput = Dialog.GetInstance().GetComponent<DialogUserInput>();
        if (userInput != null)
        {
            userInput.Freeze(this);
        }
    }

    void Update()
    {
        timerSeconds += UITime.deltaTime;
        if (timerSeconds > duration)
        {
            if (userInput != null)
            {
                userInput.Unfreeze(this);
            }
            Dialog.Advance();
            Destroy(this);
        }
    }
}
