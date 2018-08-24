using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface DialogUserInput
{
    // Forces dialog to wait by preventing user input. Used for certain cutscenes
    void Freeze(object key);
    void Unfreeze(object key);
}

public class DialogUserInputBehavior : MonoBehaviour, DialogUserInput
{
    Dialog dialog;
    DialogDisplay display;
    float fastForwardSpd = 30f;

    // when we want to prevent user input
    Dictionary<object, bool> freezes = new Dictionary<object, bool>();

    void Start()
    {
        dialog = GetComponent<Dialog>();
        display = GetComponent<DialogDisplay>();
    }

    void Update()
    {
        // do nothing if user input is frozen
        if (freezes.Count > 0)
        {
            return;
        }
        // handle user input
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Dialog.RunDialog("jungle4.protd");
        }
        if (Input.GetMouseButtonDown(0) && dialog.Active)
        {
            if (display.TextFinished())
            {
                Dialog.Advance();
            }
            else
            {
                display.AdvanceText(10f);
            }
        }
        if (Input.GetKey(KeyCode.LeftControl) && dialog.Active)
        {
            if (display.TextFinished())
            {
                Dialog.Advance();
            }
            display.AdvanceText(fastForwardSpd * UITime.deltaTime);
        }
    }

    public void Freeze(object key)
    {
        freezes[key] = true;
    }

    public void Unfreeze(object key)
    {
        freezes.Remove(key);
    }
}