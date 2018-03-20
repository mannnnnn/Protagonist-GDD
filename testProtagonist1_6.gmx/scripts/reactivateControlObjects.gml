/// reactivateControlObjects()
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
ds_list_add(controlObjects, obj_argiope);
ds_list_add(controlObjects, obj_quitter);
ds_list_add(controlObjects, obj_debugTool);
ds_list_add(controlObjects, obj_thumbnailSaver);
ds_list_add(controlObjects, obj_combat);
ds_list_add(controlObjects, obj_notifications);
ds_list_add(controlObjects, obj_particles);
ds_list_add(controlObjects, obj_map);
ds_list_add(controlObjects, obj_storyData);
ds_list_add(controlObjects, obj_music);
ds_list_add(controlObjects, obj_buttons);
ds_list_add(controlObjects, obj_triSoundLoop);

// activate the control objects
for (var i = 0; i < ds_list_size(controlObjects); i++)
{
    var obj = controlObjects[| i];
    instance_activate_object(obj);
}

// free the list
ds_list_destroy(controlObjects);
