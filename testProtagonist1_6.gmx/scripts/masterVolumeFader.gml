///masterVolumeFader(channel, volume, duration)

//destroy all old ones
with (obj_masterVolumeFader)
{
    instance_destroy();
}
// create the new fader
var fader = instance_create(0, 0, obj_masterVolumeFader);
fader.channel = argument0;
fader.finish = argument1;
fader.duration = argument2;
return fader;