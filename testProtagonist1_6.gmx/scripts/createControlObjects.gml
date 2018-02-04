/// createControlObjects()
// since they're persistent, don't create them in the room. Use this method.
// call this method at the start of the room, once (or more times) per game.

controlObjects = ds_list_create();

// add control objects here
ds_list_add(controlObjects, obj_inventory);
ds_list_add(controlObjects, obj_dialogue);
ds_list_add(controlObjects, obj_volume);
ds_list_add(controlObjects, obj_playerStats);
ds_list_add(controlObjects, obj_spellbook);
ds_list_add(controlObjects, obj_saveMenu);
ds_list_add(controlObjects, obj_loadMenu);
ds_list_add(controlObjects, obj_quitter);
ds_list_add(controlObjects, obj_debugTool);
ds_list_add(controlObjects, obj_thumbnailSaver);
ds_list_add(controlObjects, obj_combat);
ds_list_add(controlObjects, obj_notifications);
ds_list_add(controlObjects, obj_particles);
ds_list_add(controlObjects, obj_map);

// create the control objects if necessary
for (var i = 0; i < ds_list_size(controlObjects); i++)
{
    var obj = controlObjects[| i];
    // create the object only if there aren't any yet
    if (instance_number(obj) == 0)
    {
        instance_create(0, 0, obj);
    }
}

// free the list
ds_list_destroy(controlObjects);

// create a player if in a map room
var mapRoom = false;
for (var i = 0; i < ds_grid_width(obj_map.map); i++)
{
    for (var j = 0; j < ds_grid_height(obj_map.map); j++)
    {
        if (room == obj_map.map[# i, j])
        {
            mapRoom = true;
        }
    }
}
if (mapRoom)
{
    instance_create(room_width * 0.5, room_height * 0.5, obj_protagonist);
}
