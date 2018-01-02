///parseTextSpeed(line, split)
var line = argument0;
var split = argument1;

// if not enough args
if (ds_list_size(split) < 3)
{
    show_error('Error in line "' + string(line) + '": text requires 3 arguments: text option value', true);
}
var spd = real(split[| 2]);
// set text speed
obj_dialogue.display.textspeed = spd;
ds_list_destroy(split);
return false;