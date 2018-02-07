///startCombatAnimation(handler)
// play the animation that marks the beginning of combat.
var c = instance_create(0, 0, obj_combatIntro);
obj_combat.active = true;
c.handler = argument0;
// get player position and record it so we can restore it afer combat
if (instance_exists(obj_protagonist))
{
    obj_map.posX = playerX();
    obj_map.posY = playerY();
}
// stop normal music
for (var i = 0; i < ds_list_size(obj_music.music); i++)
{
    var volume = 0;
    audio_sound_gain(obj_music.music[| i], volume, 0);
}
return c;
