///getItemIndex(itemID)
// find an item's position in the inventory.
var itemID = argument0;
for (var i = 0; i < ds_list_size(obj_inventory.inventory); i++)
{
    var item = obj_inventory.inventory[| i];
    if (itemID == item[| ITEM_IDENTIFIER])
    {
        return i;
    }
}
return noone;