///parsePlaySound(line, split)
var line = argument0;
var split = argument1;

// if not enough args
if (ds_list_size(split) < 3)
{
    show_error('Error in line "' + string(line) + '": play requires 2 arguments: play channel sound', true);
}

// get args
var channel = split[| 1];
var soundName = split[| 2];

var snd = noone;
// if previous sound on this channel is playing
if (ds_map_exists(obj_dialogue.sounds, channel))
{
    snd = obj_dialogue.sounds[? channel];
    if (audio_is_playing(snd))
    {
        // stop it
        audio_stop_sound(snd);
    }
}
// play the new sound
snd = playSound(DIALOGUE, asset_get_index(soundName), false);
obj_dialogue.sounds[? channel] = snd;

// sound does not pause
ds_list_destroy(split);
return false;