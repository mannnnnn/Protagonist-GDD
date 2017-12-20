///dialogueUnhide()
if (checkDialogueHidden())
{
    obj_dialogue.display.status = NORMAL;
    if (obj_dialogue.display.state == CLOSED || obj_dialogue.display.state == CLOSING)
    {
        obj_dialogue.display.state = OPENING;
    }
    dialogueUnhideAllDrawers();
}
