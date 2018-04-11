///updateForestMusic()
var duration = 1000;
if (ds_list_empty(obj_music.music))
{
    obj_music.music[| MUSIC_FOREST1] = audio_play_sound(snd_forest1, 1, true);
    obj_music.music[| MUSIC_FOREST2] = audio_play_sound(snd_forest2, 2, true);
    obj_music.music[| MUSIC_FOREST3] = audio_play_sound(snd_forest3, 3, true);
    obj_music.music[| MUSIC_FOREST4] = audio_play_sound(snd_taut1, 4, true);
    obj_music.music[| MUSIC_FOREST5] = audio_play_sound(snd_taut2, 5, true);
    obj_music.music[| MUSIC_FOREST6] = audio_play_sound(snd_taut3, 6, true);
    obj_music.music[| MUSIC_FOREST7] = audio_play_sound(snd_apolloLoop, 7, true);
    obj_music.sfx[| MUSIC_FORESTSFX1] = audio_play_sound(snd_jungle, 1, true);
    obj_music.sfx[| MUSIC_FORESTSFX2] = audio_play_sound(snd_junglePurr, 1, true);
    duration = 1;
}

var currentRoom = obj_map.map[# obj_map.X, obj_map.Y];

// set music
obj_music.currentMusic = noone;
switch (currentRoom)
{
    case rm_forest1:
    case rm_forest2:
        obj_music.currentMusic = MUSIC_FOREST1;
        break;
    case rm_forest3:
        obj_music.currentMusic = MUSIC_FOREST1;
        // handle apollo music
        if (checkFlag("jungle4") && !checkFlag("jungle6"))
        {
            obj_music.currentMusic = MUSIC_FOREST7;
        }
        break;
    case rm_forest4:
        obj_music.currentMusic = MUSIC_FOREST2;
        // handle apollo music
        if (checkFlag("jungle4") && !checkFlag("jungle6"))
        {
            obj_music.currentMusic = MUSIC_FOREST7;
        }
        break;
    case rm_forest5:
    case rm_forest6:
        obj_music.currentMusic = MUSIC_FOREST2;
        break;
    case rm_forest7:
    case rm_forest8:
        obj_music.currentMusic = MUSIC_FOREST3;
        break;
    case rm_forest9:
        // after transform
        if (checkFlag("jungle17") && !checkFlag("sphinxxDefeated"))
        {
            obj_music.currentMusic = MUSIC_FOREST6;
        }
        else if (checkFlag("jungle15"))
        {
            obj_music.currentMusic = MUSIC_FOREST5;
        }
        else
        {
            obj_music.currentMusic = MUSIC_FOREST4;
        }
        break;
}
for (var i = 0; i < ds_list_size(obj_music.music); i++)
{
    var volume = 0;
    if (obj_music.currentMusic == i)
    {
        volume = 1;
        if (obj_music.currentMusic == MUSIC_FOREST7)
        {
            if (currentRoom == rm_forest3)
            {
                volume = 0.07;
            }
            else
            {
                volume = 0.3;
            }
        }
    }
    // stop all music while credits roll
    if (instance_exists(obj_demoEnd))
    {
        volume = 0;
    }
    audio_sound_gain(obj_music.music[| i], volume, duration);
}

// set sfx
obj_music.currentSfx = noone;
switch (currentRoom)
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
    // stop all music while credits roll
    if (instance_exists(obj_demoEnd))
    {
        volume = 0;
    }
    audio_sound_gain(obj_music.sfx[| i], volume, duration);
}
