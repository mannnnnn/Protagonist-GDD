///interactSaveGame(name, saveID)
var name = string_lower(argument0);
var saveID = argument1;

switch(name)
{
    case "save":
        saveGame(saveID);
        break;
    case "load":
        loadGame(saveID);
}