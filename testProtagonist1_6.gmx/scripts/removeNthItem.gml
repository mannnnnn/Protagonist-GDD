///removeItem(n)
// Remove the nth item in the inventory
var item = obj_inventory.inventory[| argument0];
ds_list_delete(obj_inventory.inventory, argument0);
return item;