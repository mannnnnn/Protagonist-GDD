///parseMenu(line, split)
var line = argument0;
var split = argument1;

// get menu instance name, or default
var menuObj = obj_menu;
if (ds_list_size(split) >= 2)
{
    var menuStr = split[| 1];
    if (ds_map_exists(obj_dialogue.menuKeywords, menuStr))
    {
        menuObj = obj_dialogue.menuKeywords[? menuStr];
    }
    else
    {
        menuObj = asset_get_index(menuStr);
    }
}

// check if it's a menu that actually gets passed options
// if next line isn't "{" then we're done, since it takes in nothing
if (obj_dialogue.line < ds_list_size(obj_dialogue.dialogue) - 1)
{
    if (obj_dialogue.dialogue[| obj_dialogue.line + 1] != "{")
    {
        // create the menu object and attach it to dialogue
        var menu = instance_create(0, 0, menuObj);
        // set dialogue to this menu
        dialogueSetMenu(menu);
        // pause
        ds_list_destroy(split);
        return true;
    }
}

// eventually used and freed by selectDialogueMenuOption
var optionsList = ds_list_create();
var returnPointer = noone;

// look ahead
var bracketCounter = 0;
for (var i = obj_dialogue.line; (i < ds_list_size(obj_dialogue.dialogue)) && (returnPointer = noone); i++)
{
    // bracket count
    if (obj_dialogue.dialogue[| i] == "{")
    {
        bracketCounter++;
    }
    else if (obj_dialogue.dialogue[| i] == "}")
    {
        bracketCounter--;
    }
    // find all options and their line numbers (anything bracket depth 1 followed by {)
    else if (bracketCounter == 1 && i < ds_list_size(obj_dialogue.dialogue) - 1)
    {
        if (obj_dialogue.dialogue[| i + 1] == "{")
        {
            // add this entry
            ds_list_add(optionsList, createDialogueMenuOption(unquote(string_trim(obj_dialogue.dialogue[| i])), ds_list_size(optionsList), i));
        }
    }
    // find end point
    // if we're back at original scope, we're done
    if (bracketCounter == 0 && obj_dialogue.dialogue[| i] == "}")
    {
        returnPointer = i;
    }
}
if (returnPointer == noone)
{
    show_error("Error: Bracket mismatch.", true);
}

// create the menu object and attach it to dialogue
var menu = createDialogueMenu(menuObj, optionsList, returnPointer);

// set dialogue to this menu
dialogueSetMenu(menu);

// pause (advancing isn't even allowed, unless by choosing a menu option)
ds_list_destroy(split);
return true;
