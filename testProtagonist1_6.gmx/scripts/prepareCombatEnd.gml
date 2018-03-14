/// prepareCombatEnd()
// about 2 seconds before combat ends
// prepare combat to finish (e.g. play outro, do whatever you need to do)
with (obj_combat.music)
{
    stopTriSoundLoop(id);
}