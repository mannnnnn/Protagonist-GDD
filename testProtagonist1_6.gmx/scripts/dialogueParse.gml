///dialogueParse(line)
// returns whether to pause (true) or continue (false)
var line = string_trim(argument0);

// show_debug_message("Parse: '" + line + "'");

// split the line by words
var split = ds_list_create();
string_split(string_trim(line), " ", split);
// remove any ""
for (var i = 0; i < ds_list_size(split); i++)
{
    if (split[| i] == "")
    {
        ds_list_delete(split, i);
        i--;
    }
}

// label declarations
if (split[| 0] == "label")
{
    return parseLabel(line, split);
}

// if statements
if (string_pos("if", line) == 1)
{
    return parseIf(line, split);
}

// show
if (split[| 0] == "show")
{
    return parseShow(line, split);
}

// hide
if (split[| 0] == "hide")
{
    return parseHide(line, split);
}

// scene
if (split[| 0] == "scene")
{
    return parseScene(line, split);
}

// unscene
if (split[| 0] == "unscene")
{
    return parseUnscene(line, split);
}

// with
if (split[| 0] == "with")
{
    return parseWith(line, split);
}

// now, if there's still stuff left in the dialogueQueue, execute a default with
if (!ds_list_empty(obj_dialogue.drawerQueue))
{
    var str = "with fade";
    var spl = ds_list_create();
    string_split(str, " ", spl);
    parseWith(str, spl);
    ds_list_destroy(spl);
}

// jump statement
if (split[| 0] == "jump")
{
    return parseJump(line, split);
}

// open bracket: ignore, already taken care of
if (line == "{")
{
    return parseOpenBracket(line, split);
}

// end bracket or return -> jump back to return pointer
if (line == "}" || line == "return")
{
    return parseClosedBracket(line, split);
}

// Character: Text
var pos = string_pos(":", line);
if (pos > 0)
{
    return parseCharText(line, split);
}

// Quoted Text
if (string_char_at(line, 1) == '"' && string_char_at(line, string_length(line)) == '"')
{
    return parseQuoteText(line, split);
}

// Assignment, x = true
var pos = string_pos("=", line);
if (pos > 0)
{
    return parseAssign(line, split);
}

// exit
if (line == "exit")
{
    return parseExit(line, split);
}

// pause
if (line == "pause")
{
    return parsePause(line, split);
}

// text speed
if (split[| 0] == "text" && ds_list_size(split) >= 3)
{
    if (split[| 1] == "speed")
    {
        return parseTextSpeed(line, split);
    }
    if (split[| 1] == "display")
    {
        return parseTextDisplay(line, split);
    }
    if (split[| 1] == "auto")
    {
        return parseTextAuto(line, split);
    }
}

// script call
if (split[| 0] == "call")
{
    return parseCall(line, split);
}

// stepper next
if (split[| 0] == "next")
{
    return parseNext(line, split);
}

// object creation
if (split[| 0] == "create")
{
    return parseCreate(line, split);
}

// inventory manipulation
if (split[| 0] == "inventory")
{
    return parseInv(line, split);
}

// play sound
if (split[| 0] == "play")
{
    return parsePlaySound(line, split);
}

// stop sound
if (split[| 0] == "stop")
{
    return parseStopSound(line, split);
}

// menu
if (split[| 0] == "menu")
{
    return parseMenu(line, split);
}

// wait
if (split[| 0] == "wait")
{
    return parseWait(line, split);
}

// just output text by default
return parseDefault(line, split);
