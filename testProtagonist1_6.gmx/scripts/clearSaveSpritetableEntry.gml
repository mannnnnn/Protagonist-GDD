///clearSaveSpritetableEntry(saveID)
// tells the spritetable of saveMenu to refresh an entry.
var saveID = argument0;
// free the sprite
if (sprite_exists(obj_saveMenu.spritetable[| saveID]))
{
    sprite_delete(obj_saveMenu.spritetable[| saveID]);
}
if (sprite_exists(obj_loadMenu.spritetable[| saveID]))
{
    sprite_delete(obj_loadMenu.spritetable[| saveID]);
}
// set it back to noone so that it gets re-loaded
obj_saveMenu.spritetable[| saveID] = noone;
obj_loadMenu.spritetable[| saveID] = noone;