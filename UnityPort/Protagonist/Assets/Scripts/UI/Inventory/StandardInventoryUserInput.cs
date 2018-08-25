using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UIDisplayBase;

public interface InventoryUserInput : CloseButtonTarget
{
    
}

public class StandardInventoryUserInput : MonoBehaviour, InventoryUserInput
{
    InventoryDisplay display;
    Inventory inventory;

    void Start()
    {
        inventory = transform.GetComponent<Inventory>();
        display = transform.GetComponent<InventoryDisplay>();
    }

    void Update()
    {
        // open/close
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (display.state == State.CLOSED)
            {
                display.SetState(State.OPENING);
            }
            CloseButtonClick();
        }
        // add items debug
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            int rnd = Random.Range(0, inventory.itemTypes.Count);
            ItemType item = inventory.itemTypes.ToList()[rnd].Value;
            inventory.AddItem(item);
        }
    }

    public void CloseButtonClick()
    {
        if (display.state == State.OPEN)
        {
            display.SetState(State.CLOSING);
        }
    }
}