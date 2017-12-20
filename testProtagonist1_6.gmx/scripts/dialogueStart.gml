///dialogueStart(?label, ?display)

// get jump point to start at
var jump = "";
if (argument_count >= 1 && is_string(argument[0]))
{
    jump = argument[0];
}

// change display object if necessary
var displayObj = obj_dialogueBox;
if (argument_count >= 2)
{
    displayObj = argument[1];
}
if (obj_dialogue.display.object_index != displayObj)
{
    obj_dialogue.display = instance_create(0, 0, displayObj);
}

// does nothing if already started
if (checkDialogueActive())
{
    
}
// otherwise start
else
{
    if (jump != "")
    {
        // if it doesn't exist, throw error
        if (!ds_map_exists(obj_dialogue.labels, jump))
        {
            show_error("The label " + string(jump) + " is not defined.", true);
        }
        var label = obj_dialogue.labels[? jump];
        // goto the specified line
        obj_dialogue.line = label[| LABEL_LINE];
    }
    // prepare dialogue display for start
    obj_dialogue.display.status = START;
    // start
    obj_dialogue.active = true;
    dialogueAdvance();
}
