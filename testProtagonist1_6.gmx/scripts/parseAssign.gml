///parseAssign(line, split)
var line = argument0;
var split = argument1;

// instead of split by space, split by =
var replstr = "\\][\\][";
var l = string_replace(line, "==", replstr);
split = string_split(l, '=', split);

// trim all spaces away
for (var i = 0; i < ds_list_size(split); i++)
{
    split[| i] = string_trim(split[| i]);
    if (split[| i] == "")
    {
        ds_list_delete(split, i);
        i--;
    }
}
// if not enough args
if (ds_list_size(split) < 2)
{
    show_error('Error in line "' + string(line) + '": = requires 2 arguments: variable = value', true);
}

// assign everything on the left side to the right side, right to left
for (var i = ds_list_size(split) - 1; i >= 1; i--)
{
    // get value of right side
    split[| i] = string_replace(split[| i], replstr, "==");
    var value = evalBoolean(split[| i]);
    // set left side to that value
    obj_dialogue.variables[? split[| i - 1]] = value;
}
