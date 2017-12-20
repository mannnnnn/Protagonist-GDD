///reopenOptionsMenu()
if (instance_number(obj_optionsMenu) > 0)
{
    obj_optionsMenu.state = OPENING;
    dialogueUnhide();
}