///saveStoryData
// save story data map
var fname = obj_storyData.file;
var file = file_text_open_write(fname);
// add player position data
setFlag("posX", playerX());
setFlag("posY", playerY());
setFlag("mapX", obj_map.X);
setFlag("mapY", obj_map.Y);
// add dust bunny data
var n = ds_grid_width(obj_storyData.dustBunny) * ds_grid_height(obj_storyData.dustBunny);
for (var i = 0; i < n; i++)
{
    var iX = i mod ds_grid_width(obj_storyData.dustBunny);
    var iY = floor(i / ds_grid_width(obj_storyData.dustBunny));
    setFlag("dustBunny" + string(i), obj_storyData.dustBunny[# iX, iY]);
    setFlag("pickedUp" + string(i), obj_storyData.pickedUp[# iX, iY]);
    setFlag("interaction" + string(i), json_encode(obj_storyData.interaction[# iX, iY]));
}
// add inventory data
var invMap = ds_map_create();
// create a copy
var invList = ds_list_create();
for (var i = 0; i < ds_list_size(obj_inventory.inventory); i++)
{
    var item = obj_inventory.inventory[| i];
    invList[| i] = item[| ITEM_IDENTIFIER];
}
ds_map_add_list(invMap, "0", invList);
setFlag("inventory", json_encode(invMap));
ds_map_destroy(invMap);
// save
file_text_write_string(file, json_encode(obj_storyData.data));
file_text_writeln(file);
file_text_close(file);
