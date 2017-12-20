///dialogueHideAllDrawers()
// hide all dialogue system drawers.

var current = ds_map_find_first(obj_dialogue.drawers);
var size = ds_map_size(obj_dialogue.drawers);
for (var i = 0; i < size; i++)
{
    var drawer = obj_dialogue.drawers[? current];
    // if it exists still
    if (instance_exists(drawer))
    {
        // hide it
       hideDrawer(drawer);
    }
    current = ds_map_find_next(obj_dialogue.drawers, current);
}