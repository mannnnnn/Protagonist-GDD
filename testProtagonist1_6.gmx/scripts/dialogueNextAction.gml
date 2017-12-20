///dialogueNextAction()
// returns the value of dialogueParse(str)

// if just starting
if (obj_dialogue.line < 0 && ds_list_size(obj_dialogue.dialogue) > 0)
{
    // starting line
    obj_dialogue.line = 0;
}
// if parsing
else
{
    // next line
    obj_dialogue.line++;
    // if out of bounds, stop
    if (obj_dialogue.line >= ds_list_size(obj_dialogue.dialogue))
    {
        return true;
    }
}
// execute
return dialogueParse(obj_dialogue.dialogue[| obj_dialogue.line]);