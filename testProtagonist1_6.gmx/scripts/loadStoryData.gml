///loadStoryData(?file)
/// load story data map
var datafile = obj_storyData.file;
if (argument_count >= 1)
{
    datafile = argument[0];
}
var fname = datafile;
if (!file_exists(fname))
{
    return false;
}
var file = file_text_open_read(fname);
ds_map_destroy(obj_storyData.data);
obj_storyData.data = json_decode(file_text_read_string(file));

// load player position data
if (ds_map_exists(obj_storyData.data, "posX"))
{
    obj_map.posX = checkFlag("posX");
}
if (ds_map_exists(obj_storyData.data, "posY"))
{
    obj_map.posY = checkFlag("posY");
}
if (ds_map_exists(obj_storyData.data, "mapX"))
{
    obj_map.X = checkFlag("mapX");
}
if (ds_map_exists(obj_storyData.data, "mapY"))
{
    obj_map.Y = checkFlag("mapY");
}
// help data
if (ds_map_exists(obj_storyData.data, "help"))
{
    obj_map.help = checkFlag("help");
}
if (ds_map_exists(obj_storyData.data, "helpSpr"))
{
    obj_map.helpSpr = checkFlag("helpSpr");
}
if (ds_map_exists(obj_storyData.data, "helpImg"))
{
    obj_map.helpImg = checkFlag("helpImg");
}
if (ds_map_exists(obj_storyData.data, "helpTarget"))
{
    obj_map.helpTarget = checkFlag("helpTarget");
}
// load dust bunny data
var n = ds_grid_width(obj_storyData.dustBunny) * ds_grid_height(obj_storyData.dustBunny);
for (var i = 0; i < n; i++)
{
    var iX = i mod ds_grid_width(obj_storyData.dustBunny);
    var iY = floor(i / ds_grid_width(obj_storyData.dustBunny));
    if (ds_map_exists(obj_storyData.data, "dustBunny" + string(i)))
    {
        obj_storyData.dustBunny[# iX, iY] = checkFlag("dustBunny" + string(i));
    }
    if (ds_map_exists(obj_storyData.data, "pickedUp" + string(i)))
    {
        obj_storyData.pickedUp[# iX, iY] = checkFlag("pickedUp" + string(i));
    }
    if (ds_map_exists(obj_storyData.data, "interaction" + string(i)))
    {
        if (ds_exists(obj_storyData.interaction[# iX, iY], ds_type_map))
        {
            ds_map_destroy(obj_storyData.interaction[# iX, iY]);
        }
        obj_storyData.interaction[# iX, iY] = json_decode(checkFlag("interaction" + string(i)));
    }
}

// load inventory data
if (ds_map_exists(obj_storyData.data, "inventory"))
{
    var invMap = json_decode(checkFlag("inventory"));
    var list = invMap[? "0"];
    ds_list_clear(obj_inventory.inventory);
    for (var i = 0; i < ds_list_size(list); i++)
    {
        ds_list_add(obj_inventory.inventory, obj_inventory.items[? list[| i]]);
    }
    ds_map_destroy(invMap);
}

file_text_readln(file);
file_text_close(file);

return true;
