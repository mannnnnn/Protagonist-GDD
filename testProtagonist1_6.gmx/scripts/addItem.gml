///addItem(itemID)
var itemID = argument0;
if (ds_map_exists(obj_inventory.items, itemID))
{
    ds_list_add(obj_inventory.inventory, obj_inventory.items[? itemID]);
    return ds_list_size(obj_inventory.inventory) - 1;
}
return noone;