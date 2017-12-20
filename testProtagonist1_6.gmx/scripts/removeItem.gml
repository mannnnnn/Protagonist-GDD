///removeItem(itemID)
var itemID = argument0;
if (ds_map_exists(obj_inventory.items, itemID))
{
    // find index
    var ind = getItemIndex(itemID);
    // if valid index
    if (ind >= 0)
    {
        // remove
        removeNthItem(ind);
    }
    return ind;
}
return noone;