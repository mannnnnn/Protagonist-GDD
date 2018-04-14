///createCharacter(name, displayName, textspeed, side)
var characterMarker = ds_list_create();
characterMarker[| CHARACTER_NAME] = argument0;
characterMarker[| CHARACTER_DISPLAYNAME] = argument1;
characterMarker[| CHARACTER_TEXTSPEED] = argument2;
characterMarker[| CHARACTER_SIDE] = argument3;
return characterMarker;
