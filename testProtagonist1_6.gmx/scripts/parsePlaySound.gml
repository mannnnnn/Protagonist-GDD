///parsePlaySound(line, split)
var line = argument0;
var split = argument1;

// if not enough args
if (ds_list_size(split) < 3)
{
    show_error('Error in line "' + string(line) + '": play requires 2 arguments: play channel sound ?intro ?outro', true);
}

// get args
var channel = split[| 1];
var playLoop = false;
var soundName = asset_get_index(split[| 2]);
var introName = noone;
if (ds_list_size(split) >= 4 && split[| 3] != "noone")
{
    playLoop = true;
    introName = asset_get_index(split[| 3]);
}
var outroName = noone;
if (ds_list_size(split) >= 5 && split[| 4] != "noone")
{
    playLoop = true;
    outroName = asset_get_index(split[| 4]);
}

var snd = noone;
// if previous sound on this channel is playing
if (ds_map_exists(obj_dialogue.sounds, channel))
{
    snd = obj_dialogue.sounds[? channel];
    if (instance_exists(snd))
    {
        // stop it
        if (snd.state != CLOSING)
        {
            snd.state = CLOSING;
        }
    }
}
// play the new sound
snd = createTriSoundLoop(introName, soundName, outroName);
obj_dialogue.sounds[? channel] = snd;
snd.playLoop = playLoop;

// sound does not pause
ds_list_destroy(split);
return false;
