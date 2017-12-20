///parseStopSound(line, split)
var line = argument0;
var split = argument1;

// if not enough args
if (ds_list_size(split) < 2)
{
    show_error('Error in line "' + string(line) + '": stop requires 1 arguments: stop channel', true);
}

// get arg
var channel = split[| 1];

// if previous sound on this channel is playing
if (ds_map_exists(obj_dialogue.sounds, channel))
{
    var snd = obj_dialogue.sounds[? channel];
    if (audio_is_playing(snd))
    {
        // stop it
        audio_stop_sound(snd);
    }
}

// stop sound does not pause
ds_list_destroy(split);
return false;