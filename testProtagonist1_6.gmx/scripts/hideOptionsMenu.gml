///hideOptionsMenu()
if (instance_number(obj_optionsMenu) > 0)
{
    obj_optionsMenu.state = CLOSING;
    dialogueHide();
}