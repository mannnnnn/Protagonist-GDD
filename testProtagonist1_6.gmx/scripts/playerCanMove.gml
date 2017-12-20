///playerCanMove()
// determines whether or not the player should be allowed to move.

// if inventory open, don't move.
if (instance_exists(obj_inventory) && obj_inventory.state != CLOSED)
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

// if nothing is preventing the player from moving, then return true.
return true;
