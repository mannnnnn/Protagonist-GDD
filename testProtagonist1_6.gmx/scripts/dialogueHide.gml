///dialogueHide()
if (!checkDialogueHidden())
{
    obj_dialogue.display.status = HIDE;
    if (obj_dialogue.display.state == OPEN || obj_dialogue.display.state == OPENING)
    {
        obj_dialogue.display.state = CLOSING;
    }
    dialogueHideAllDrawers();
}
