///parseExit(line, split)
var line = argument0;
var split = argument1;

// default case: juse print out the line as usual
var text = line;
// set dialogue scroll text thing
dialogueTextBlock(text, "");
// true to pause
ds_list_destroy(split);
return true;