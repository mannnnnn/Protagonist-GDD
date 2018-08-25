using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UIDisplayBase;

/**
 * Similar to InventoryUserInput, except it is created in an event
 * which forces the player to make room for an item before being closed.
 * Then, resets the inventory user input back to normal.
 */
public class InventoryUserInputRemove : MonoBehaviour, InventoryUserInput
{
    InventoryDisplay display;
    Inventory inventory;

    void Start()
    {
        inventory = transform.GetComponent<Inventory>();
        display = transform.GetComponent<InventoryDisplay>();
        // if inventory isn't full, we're done
        if (!inventory.IsFull)
        {
            Finish();
            return;
        }
        display.SetState(State.OPENING);
    }

    void Update()
    {
        // open/close
        if (Input.GetKeyDown(KeyCode.X))
        {
            CloseButtonClick();
        }

        // remove items debug
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            inventory.RemoveItem(inventory.items.ToList().First(x => x.Value != null && x.Value.Count > 0).Value.First());
        }
    }

    private void Finish()
    {
        gameObject.AddComponent<InventoryUserInputStandard>();
        Destroy(this);
    }

    public void CloseButtonClick()
    {
        if (display.state == State.OPEN && !inventory.IsFull)
        {
            // once the inventory is no longer full, we're done
            display.SetState(State.CLOSING);
            Finish();
            return;
        }
    }
}