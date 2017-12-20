///parseExit(line, split)
var line = argument0;
var split = argument1;

// if not enough args
if (ds_list_size(split) < 2)
{
    show_error('Error in line "' + string(line) + '": create requires at least 1 argument: create obj_name ?X ?Y', true);
}
var objName = split[| 1];
if (objName == "stepper")
{
    return parseCreateStepper(line, split);
}
// if it has position args
var objX = 0;
var objY = 0;
if (ds_list_size(split) >= 4)
{
    objX = real(split[| 2]);
    objY = real(split[| 3]);
}
// objects, by default, do not pause
obj_dialogue.scriptPause = false;
// create the object
instance_create(objX, objY, asset_get_index(objName));
// return scriptPause, which can be set by the object to control its own pause/continue
ds_list_destroy(split);
return obj_dialogue.scriptPause;
