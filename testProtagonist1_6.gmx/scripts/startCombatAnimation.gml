///startCombatAnimation(handler)
// play the animation that marks the beginning of combat.
var c = instance_create(0, 0, obj_combatIntro);
obj_combat.active = true;
c.handler = argument0;

// transfer help data from obj_map to this
if (obj_map.help)
{
    var help = false;
    if (obj_map.helpTarget == noone)
    {
        help = true;
    }
    else if (obj_map.helpTarget == rm_test2 && c.handler != obj_combatPuzzle)
    {
        help = true;
    }
    else if (obj_map.helpTarget == rm_boss1 && c.handler == obj_combatPuzzle)
    {
        help = true;
    }
    if (help)
    {
        obj_map.help = false;
        c.pause = true;
        c.pauseSprite = obj_map.helpSpr;
        c.pauseImage = obj_map.helpImg;
    }
}

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
