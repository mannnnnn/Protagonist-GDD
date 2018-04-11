///createItem(identifier, sprite, image, name, desc, usable)

// make sure it doesn't already exist
if (ds_map_exists(obj_inventory.items, argument[0]))
{
    show_error("Item with identifier " + argument[0] + " already exists.", true);
}

// create the item
var item = ds_list_create();
item[| ITEM_IDENTIFIER] = argument[0];
item[| ITEM_SPRITE] = argument[1];
item[| ITEM_IMAGE] = argument[2];
item[| ITEM_NAME] = argument[3];
item[| ITEM_DESC] = argument[4];
item[| ITEM_USABLE] = argument[5];

// add to inventory item types list
obj_inventory.items[? argument[0]] = item;
return item;
