///parseExit(line, split)
var line = argument0;
var split = argument1;

// if not enough args
if (ds_list_size(split) < 3)
{
    show_error('Error in line "' + string(line) + '": inventory requires 2 arguments: inventory add/remove itemID', true);
}
// get args
var action = split[| 1];
var itemID = split[| 2];
// execute action, add or remove
switch (action)
{
    case "add":
        addItem(itemID);
        break;
    case "remove":
        removeItem(itemID);
        break;
    default:
        show_error("Unsupported inventory action: " + string(action), true);
        break;
}
// return false, inventory does not pause
ds_list_destroy(split);
return false;
