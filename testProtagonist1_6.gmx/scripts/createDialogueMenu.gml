///createDialogueMenu(obj, optionsList, returnPointer)
var obj = argument0;
var optionsList = argument1;
var returnPointer = argument2;

// create menu
var menu = instance_create(0, 0, obj);
menu.optionsList = optionsList;
menu.returnPointer = returnPointer;

return menu;