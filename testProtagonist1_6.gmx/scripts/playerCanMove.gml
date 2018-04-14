///playerCanMove()
// determines whether or not the player should be allowed to move.
// if inventory open, don't move.
if (instance_exists(obj_inventory) && obj_inventory.state != CLOSED)
{
    return false;
}

// if waiting for a post puzzle trigger, player can't move
if (instance_exists(obj_triggerPostPuzzle))
{
    return false;
}

// if dialogue is active, don't move
if (instance_exists(obj_dialogue) && obj_dialogue.active)
{
    return false;
}

// if combat intro animation is active, don't move
if (instance_exists(obj_combatIntro))
{
    return false;
}

// if transitioning rooms, don't move
if (instance_exists(obj_roomTransition))
{
    return false;
}

// if transitioning rooms, don't move
if (instance_exists(obj_triggerPostSphinx))
{
    return false;
}

// do nothing if credits are rolling
if (instance_exists(obj_demoEnd))
{
    return false;
}

if (instance_exists(obj_getStabbed))
{
    return false;
}

// if nothing is preventing the player from moving, then return true.
return instance_exists(obj_protagonist);
