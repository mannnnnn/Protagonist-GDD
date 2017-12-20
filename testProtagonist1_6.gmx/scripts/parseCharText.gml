///parseCharText(line, split)
var line = argument0;
var split = argument1;

// then split by colon
string_split(line, ":", split);
var text = unquote(string_trim(split[| 1]));
var character = string_trim(split[| 0]);
if (!ds_map_exists(obj_dialogue.characters, character))
{
    show_error("The character " + string(character) + " is not defined.", true);
}
var char = obj_dialogue.characters[? character];
var name = char[| CHARACTER_DISPLAYNAME];
// set dialogue scroll text thing
dialogueTextBlock(text, name);
// true to pause
ds_list_destroy(split);
return true;
