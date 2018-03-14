///parseTextDisplay(line, split)
var line = argument0;
var split = argument1;

// if not enough args
if (ds_list_size(split) < 3)
{
    show_error('Error in line "' + string(line) + '": text requires 3 arguments: text option value', true);
}
var obj = asset_get_index(split[| 2]);
// set display obj typ
swapDialogueDisplay(obj);
ds_list_destroy(split);
return false;