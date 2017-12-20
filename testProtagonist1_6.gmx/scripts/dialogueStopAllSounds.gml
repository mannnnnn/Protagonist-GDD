///dialogueStopAllSounds()
// stop all dialogue system sounds

var current = ds_map_find_first(obj_dialogue.sounds);
var size = ds_map_size(obj_dialogue.sounds);
for (var i = 0; i < size; i++)
{
    var snd = obj_dialogue.sounds[? current];
    // if it's playing
    if (audio_is_playing(snd))
    {
        // stop it
        audio_stop_sound(snd);
    }
    current = ds_map_find_next(obj_dialogue.sounds, current);
}
// clear the map
ds_map_clear(obj_dialogue.sounds);