///checkDialogueActive()
// checks if the dialogue window is open
if (obj_dialogue.display.state != CLOSED)
{
    return false;
}
return obj_dialogue.active;
