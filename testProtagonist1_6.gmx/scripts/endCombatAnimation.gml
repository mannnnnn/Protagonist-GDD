///endCombatAnimation()
// play the animation that marks the end of combat.
// kicks the player back to the map.
createRoomTransition(obj_map.map[# obj_map.X, obj_map.Y], obj_fadeTransition);
// start normal music again
for (var i = 0; i < ds_list_size(obj_music.music); i++)
{
    var volume = 0;
    if (i == obj_music.current)
    {
        volume = 1;
    }
    audio_sound_gain(obj_music.music[| i], volume, 0);
}
