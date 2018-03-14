///playSound(channel, sound, ?loop)
var channel = argument[0];
var sound = argument[1];
var loop = false;
if (argument_count >= 3)
{
    loop = argument[2];
}
var snd = audio_play_sound(sound, channel, loop);
ds_list_add(obj_volume.sounds[| channel], snd);
return snd;