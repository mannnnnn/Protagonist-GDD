using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Libraries.ProtagonistDialog;
using UnityEngine;
using static UIDisplayBase;

/**
 * Menu called by dialog when the inventory needs to be not full.
 * Calls dialog.ChooseMenuOption when the inventory is closed.
 * Can only be closed when not full.
 * Instantly calls dialog.ChooseMenuOption if inventory is not full at the start.
 */
public class CheckInventoryMenu : MonoBehaviour, DialogMenu
{
    bool finished = false;
    DialogParser dialog;
    DialogTarget target;
    public List<Dictionary<string, object>> Initialize(List<Dictionary<string, object>> options, DialogParser dialog, DialogTarget target, DialogDisplay display)
    {
        this.dialog = dialog;
        this.target = target;
        return options;
    }

    void Start()
    {
        // if we have a non-full inventory, we're done
        if (!Inventory.GetInstance().IsFull)
        {
            dialog.ChooseMenuOption(0, target);
            finished = true;
            return;
        }
        // otherwise, create the inventory user input behavior for forced-removal
        // this behavior swaps itself back to the normal one
        MonoBehaviour userInput =
            Inventory.GetInstance().gameObject.GetComponent<InventoryUserInput>() as MonoBehaviour;
        if (userInput != null)
        {
            Destroy(userInput);
        }
        Inventory.GetInstance().gameObject.AddComponent<InventoryUserInputRemove>();
    }

    // when we close, we're done
    void Update()
    {
        if (!finished && Inventory.GetInstance().display.state == State.CLOSED)
        {
            dialog.ChooseMenuOption(0, target);
            finished = true;
            Destroy(gameObject);
        }
    }
}