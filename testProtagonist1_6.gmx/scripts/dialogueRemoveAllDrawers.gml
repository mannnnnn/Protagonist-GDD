///dialogueRemoveAllDrawers()
// self-destruct all dialogue system drawers.

var current = ds_map_find_first(obj_dialogue.drawers);
var size = ds_map_size(obj_dialogue.drawers);
for (var i = 0; i < size; i++)
{
    var drawer = obj_dialogue.drawers[? current];
    // if it exists still
    if (instance_exists(drawer))
    {
        // self-destruct it
        removeDrawer(drawer);
    }
    current = ds_map_find_next(obj_dialogue.drawers, current);
}
// clear the map
ds_map_clear(obj_dialogue.drawers);