///parseQuoteText(line, split)
var line = argument0;
var split = argument1;

var text = unquote(line);
// set dialogue scroll text thing
dialogueTextBlock(text, "");
// true to pause
ds_list_destroy(split);
return true;