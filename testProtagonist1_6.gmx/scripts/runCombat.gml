///runCombat()
// run from combat
endCombat();

// fade out combat music
obj_combat.music.outro = noone;
obj_combat.music.playLoop = false;
audio_sound_gain(obj_combat.music.loopSnd, 0, 1000);
stopTriSoundLoop(obj_combat.music);
