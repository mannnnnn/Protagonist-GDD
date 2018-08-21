using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface DialogUserInput
{

}

public class DialogUserInputBehavior : MonoBehaviour, DialogUserInput
{
    DialogBehavior dialogBehavior;
    DialogDisplay display;
    float fastForwardSpd = 15f;

    void Start()
    {
        dialogBehavior = GetComponent<DialogBehavior>();
        display = GetComponent<DialogDisplay>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            DialogBehavior.RunDialog("testcase.protd");
        }
        if (Input.GetMouseButtonDown(0) && display.active)
        {
            dialogBehavior.dialog.Run(dialogBehavior);
        }
        if (Input.GetKey(KeyCode.LeftControl) && display.active)
        {
            if (display.TextFinished())
            {
                dialogBehavior.dialog.Run(dialogBehavior);
            }
            display.AdvanceText(fastForwardSpd * UITime.deltaTime);
        }
    }
}