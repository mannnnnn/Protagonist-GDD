///checkItem(itemID)
var itemID = argument0;
// check to see if an item is in the inventory. O(n) operation.
for (var i = 0; i < ds_list_size(obj_inventory.inventory); i++)
{
    var item = obj_inventory.inventory[| i];
    if (itemID == item[| ITEM_IDENTIFIER])
    {
        return true;
    }
}
return false;
