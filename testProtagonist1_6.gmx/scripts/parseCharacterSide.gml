///parseCharacterSide(line, split)
var line = argument0;
var split = argument1;

// if not enough args
if (ds_list_size(split) < 4)
{
    show_error('Error in line "' + string(line) + '": text side requires 2 arguments: text side channel value', true);
}
var channel = split[| 2];
var side = string_lower(split[| 3]);
var sideConst = noone;
switch (side)
{
    case "left":
        sideConst = LEFT;
        break;
    case "right":
        sideConst = RIGHT;
        break;
    default:
        show_error('Error in line "' + string(line) + '": Invalid side "' + side + '" is not "right" or "left"', true);
        break;
}
// set character side
if (!ds_map_exists(obj_dialogue.characters, channel))
{
    show_error("The character " + string(channel) + " is not defined.", true);
}
var char = obj_dialogue.characters[? channel];
char[| CHARACTER_SIDE] = sideConst;

ds_list_destroy(split);
return false;
