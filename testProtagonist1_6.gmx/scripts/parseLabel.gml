///parseLabel(line, split)
var line = argument0;
var split = argument1;

// check which label type it is
var name = split[| 1];
var label = obj_dialogue.labels[? name];
var labeltype = label[| LABEL_TYPE];

// if it's a goto, just skip it
if (labeltype == LABEL_TYPE_GOTO)
{
    // just do nothing and continue
}
// if bracketed label, jump to end bracket
else
{
    // look ahead
    var bracketCounter = 0;
    var jumpPointer = noone;
    for (var i = obj_dialogue.line; (i < ds_list_size(obj_dialogue.dialogue)) && (jumpPointer = noone); i++)
    {
        // bracket count
        if (obj_dialogue.dialogue[| i] == "{")
        {
            bracketCounter++;
        }
        else if (obj_dialogue.dialogue[| i] == "}")
        {
            bracketCounter--;
        }
        // find end point
        // if we're back at original scope, we're done
        if (bracketCounter == 0 && obj_dialogue.dialogue[| i] == "}")
        {
            jumpPointer = i;
        }
    }
    if (jumpPointer == noone)
    {
        show_error("Error: Bracket mismatch.", true);
    }
    obj_dialogue.line = jumpPointer;
}

// continue on like nothing happened
ds_list_destroy(split);
return false;
