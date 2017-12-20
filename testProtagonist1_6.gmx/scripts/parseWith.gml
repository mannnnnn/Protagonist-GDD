///parseWith(line, split)
// apply drawer to all previous shows/hides.
var line = argument0;
var split = argument1;

if (ds_list_size(split) < 2)
{
    show_error('Error in line "' + string(line) + '": with requires at least 1 argument: with effect', true);
}

var type = split[| 1];

// for every entry in the drawer queue
for (var i = 0; i < ds_list_size(obj_dialogue.drawerQueue); i++)
{
    var entry = obj_dialogue.drawerQueue[| i];
    createDrawer(entry, type);
    // destroy the entry afterwards, since it's already been used.
    ds_list_destroy(entry);
}
ds_list_clear(obj_dialogue.drawerQueue);

// no need to pause
ds_list_destroy(split);
return false;