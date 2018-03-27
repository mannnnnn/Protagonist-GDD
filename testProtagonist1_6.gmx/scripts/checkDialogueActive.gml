///checkDialogueActive()
// checks if the dialogue window is open
if (obj_dialogue.display.state != CLOSED)
{
    return true;
}
return obj_dialogue.active;
