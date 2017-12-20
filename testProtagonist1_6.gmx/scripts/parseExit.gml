///parseExit(line, split)
var line = argument0;
var split = argument1;

// exit
dialogueStop();
// pause
ds_list_destroy(split);
return true;