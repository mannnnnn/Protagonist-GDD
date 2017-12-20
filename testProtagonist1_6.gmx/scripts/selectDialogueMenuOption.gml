///selectDialogueMenuOption(optionsList, index, returnPointer)
// selects the nth indexed dialogue menu option.

var optionsList = argument0;
var index = argument1;
var option = optionsList[| index];
var returnPointer = argument2;

// push stack to end of menu
ds_stack_push(obj_dialogue.jumpstack, returnPointer);

// goto the option's block line
obj_dialogue.line = option[| MENU_OPTION_LINE];

// dialogue is no longer in menu mode
dialogueSetMenu(noone);

// free all options in optionsList
for (var i = 0; i < ds_list_size(optionsList); i++)
{
    ds_list_destroy(optionsList[| i]);
}
// free optionsList
ds_list_destroy(optionsList);

// advance
dialogueAdvance();