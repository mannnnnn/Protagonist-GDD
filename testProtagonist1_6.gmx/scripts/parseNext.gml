///parseNext(line, split)
var line = argument0;
var split = argument1;

// if not enough args
if (ds_list_size(split) < 2)
{
    show_error('Error in line "' + string(line) + '": next requires 2 arguments: next channel', true);
}

// get args
var channel = split[| 1];

// steppers, by default, do not pause
obj_dialogue.scriptPause = false;

// next
if (ds_map_exists(obj_dialogue.steppers, channel) && instance_exists(obj_dialogue.steppers[? channel]))
{
    with(obj_dialogue.steppers[? channel])
    {
        event_user(0);
    }
}
else
{
    show_error('Error in line "' + string(line) + '": stepper on that channel does not exist or has been destroyed', true);
}

// return scriptPause, which can be set by the stepper to control its own pause/continue
ds_list_destroy(split);
return obj_dialogue.scriptPause;
