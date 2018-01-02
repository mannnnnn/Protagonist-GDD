///dialogueStop()
// deactivate
obj_dialogue.active = false;
obj_dialogue.display.status = STOP;
// close window
if (obj_dialogue.display.state == OPEN || obj_dialogue.display.state == OPENING)
{
    obj_dialogue.display.state = CLOSING;
}
// reset instruction number
obj_dialogue.line = noone;

// remove all image drawers
dialogueRemoveAllDrawers();

// stop all sounds
dialogueStopAllSounds();

// remove all steppers
dialogueRemoveAllSteppers();

// clear drawer queue
ds_list_clear(obj_dialogue.drawerQueue);

// clear the jumpstack
ds_stack_clear(obj_dialogue.jumpstack);