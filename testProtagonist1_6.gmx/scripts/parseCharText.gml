///parseCharText(line, split)
var line = argument0;
var split = argument1;

// then split by colon
string_split(line, ":", split);
var character = string_trim(split[| 0]);
ds_list_delete(split, 0);
var text = unquote(string_trim(string_join(split, ":")));
if (!ds_map_exists(obj_dialogue.characters, character))
{
    show_error("The character " + string(character) + " is not defined.", true);
}
var char = obj_dialogue.characters[? character];
var name = char[| CHARACTER_DISPLAYNAME];
// set dialogue scroll text thing
dialogueTextBlock(text, name, char[| CHARACTER_SIDE]);

// true to pause
ds_list_destroy(split);
return true;
