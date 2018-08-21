using Assets.Scripts.UI.Inventory;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UIDisplayBase;

public interface InventoryUserInput
{

}

public class InventoryUserInputBehavior : MonoBehaviour
{
    InventoryDisplayBehavior display;
    Inventory inventory;

    void Start()
    {
        inventory = transform.GetComponent<Inventory>();
        display = transform.GetComponent<InventoryDisplayBehavior>();
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
            if (display.state == State.OPEN)
            {
                display.SetState(State.CLOSING);
            }
        }
        // add items debug
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            int rnd = Random.Range(0, inventory.itemTypes.Count);
            ItemType item = inventory.itemTypes.ToList()[rnd].Value;
            inventory.AddItem(item);
        }
    }
}