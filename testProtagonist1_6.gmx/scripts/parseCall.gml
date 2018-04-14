///parseExit(line, split)
var line = argument0;
var split = argument1;

// if not enough args
if (ds_list_size(split) < 2)
{
    show_error('Error in line "' + string(line) + '": call requires 1 argument: call scriptName()', true);
}
// now take the rest of the line excluding "call"
ds_list_delete(split, 0);
var call = string_join(split, " ");
// now split script from args
string_split(call, "(", split);
// get script name
var scriptName = split[| 0];
ds_list_delete(split, 0);
// now parse args
var stringArgs = unparen("(" + string_join(split, "("));
string_split(stringArgs, ",", split);
// convert to values
for (var i = 0; i < ds_list_size(split); i++)
{
    split[| i] = stringToValue(string_trim(split[| i]), line);
}
// scripts, by default, do not pause
obj_dialogue.scriptPause = false;
// call script
executeScript(asset_get_index(scriptName), split);
// return scriptPause, which can be set by the script to control its own pause/continue
ds_list_destroy(split);
return obj_dialogue.scriptPause;
