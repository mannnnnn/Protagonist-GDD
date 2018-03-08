///updateForestMusic()
var duration = 1000;
if (ds_list_empty(obj_music.music))
{
    obj_music.music[| MUSIC_FOREST1] = audio_play_sound(snd_forest1, 1, true);
    obj_music.music[| MUSIC_FOREST2] = audio_play_sound(snd_forest2, 2, true);
    obj_music.music[| MUSIC_FOREST3] = audio_play_sound(snd_forest3, 3, true);
    obj_music.sfx[| MUSIC_FORESTSFX1] = audio_play_sound(snd_jungle, 1, true);
    obj_music.sfx[| MUSIC_FORESTSFX2] = audio_play_sound(snd_junglePurr, 1, true);
    duration = 0;
}

// set music
obj_music.currentMusic = noone;
switch (obj_map.map[# obj_map.X, obj_map.Y])
{
    case rm_forest1:
    case rm_forest2:
    case rm_forest3:
        obj_music.currentMusic = MUSIC_FOREST1;
        break;
    case rm_forest4:
    case rm_forest5:
    case rm_forest6:
        obj_music.currentMusic = MUSIC_FOREST2;
        break;
    case rm_forest7:
    case rm_forest8:
    case rm_forest9:
        obj_music.currentMusic = MUSIC_FOREST3;
        break;
}
for (var i = 0; i < ds_list_size(obj_music.music); i++)
{
    var volume = 0;
    if (obj_music.currentMusic == i)
    {
        volume = 1;
    }
    audio_sound_gain(obj_music.music[| i], volume, duration);
}

// set sfx
obj_music.currentSfx = noone;
switch (obj_map.map[# obj_map.X, obj_map.Y])
{
    case rm_forest1:
    case rm_forest2:
    case rm_forest3:
    case rm_forest4:
    case rm_forest5:
    case rm_forest6:
    case rm_forest7:
    case rm_forest8:
        obj_music.currentSfx = MUSIC_FORESTSFX1;
        break;
    case rm_forest9:
        obj_music.currentSfx = MUSIC_FORESTSFX2;
        break;
}
for (var i = 0; i < ds_list_size(obj_music.sfx); i++)
{
    var volume = 0;
    if (obj_music.currentSfx == i)
    {
        volume = 1;
    }
    audio_sound_gain(obj_music.sfx[| i], volume, duration);
}
