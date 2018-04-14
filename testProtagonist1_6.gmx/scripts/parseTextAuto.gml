///parseTextAuto(line, split)
var line = argument0;
var split = argument1;

// if not enough args
if (ds_list_size(split) < 3)
{
    show_error('Error in line "' + string(line) + '": text requires 3 arguments: text option value', true);
}
var val = stringToValue(split[| 2], line);
// set display text auto-advance
if (is_real(val))
{
    obj_dialogue.display.autoAdvance = val;
}
return false;
