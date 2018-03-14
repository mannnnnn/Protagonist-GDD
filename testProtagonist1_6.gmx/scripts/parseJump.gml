///parseJump(line, split)
var line = argument0;
var split = argument1;

// if not enough args
if (ds_list_size(split) < 2)
{
    show_error('Error in line "' + string(line) + '": jump requires 1 argument: jump labelName', true);
}
var jump = string_trim(split[| 1]);
// check if label exists. If it does, access it.
if (!ds_map_exists(obj_dialogue.labels, jump))
{
    show_error("The label " + string(jump) + " is not defined.", true);
}
var label = obj_dialogue.labels[? jump];
// if goto, just go
if (label[| LABEL_TYPE] == LABEL_TYPE_GOTO)
{
    // goto the specified line
    obj_dialogue.line = label[| LABEL_LINE];
}
// if call
else
{
    // push current line onto stack of return pointers
    ds_stack_push(obj_dialogue.jumpstack, obj_dialogue.line);
    // jump to label
    obj_dialogue.line = label[| LABEL_LINE];
}
ds_list_destroy(split);
return false;