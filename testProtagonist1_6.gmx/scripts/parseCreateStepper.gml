///parseCreateStepper(line, split)
var line = argument0;
var split = argument1;

// if not enough args
if (ds_list_size(split) < 4)
{
    show_error('Error in line "' + string(line) + '": create stepper requires at least 2 arguments: create stepper channel obj_name ?X ?Y', true);
}

// channel
var channel = split[| 2];

// object name
var objName = split[| 3];

// if it has position args
var objX = 0;
var objY = 0;
if (ds_list_size(split) >= 6)
{
    objX = real(split[| 4]);
    objY = real(split[| 5]);
}

// steppers, by default, do not pause
obj_dialogue.scriptPause = false;
// create the stepper
var stepper = instance_create(objX, objY, asset_get_index(objName));
// register the stepper in the desired channel
dialogueAddStepper(stepper, channel);
// return scriptPause, which can be set by the stepper to control its own pause/continue
ds_list_destroy(split);
return obj_dialogue.scriptPause;