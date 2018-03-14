///dialogueStopAllSounds()
// stop all dialogue system sounds

var current = ds_map_find_first(obj_dialogue.sounds);
var size = ds_map_size(obj_dialogue.sounds);
for (var i = 0; i < size; i++)
{
    var snd = obj_dialogue.sounds[? current];
    // if it's playing
    if (instance_exists(snd))
    {
        // stop it
        if (snd.state != CLOSING)
        {
            snd.state = CLOSING;
        }
    }
    current = ds_map_find_next(obj_dialogue.sounds, current);
}
// clear the map
ds_map_clear(obj_dialogue.sounds);