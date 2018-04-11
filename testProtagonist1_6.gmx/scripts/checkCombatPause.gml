/// checkCombatPause()
// returns true if combat should be paused
if (instance_exists(obj_jokeTestAth))
{
    return false;
}
if (checkDialogueActive())
{
    return true;
}
return !checkCombatActive();
