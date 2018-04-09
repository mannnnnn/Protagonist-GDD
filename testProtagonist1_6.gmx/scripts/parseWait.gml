///parseWait()
var line = argument0;
var split = argument1;

if (ds_list_size(split) < 2)
{
    show_error('Error in line "' + string(line) + '": wait requires at least 1 argument: wait steps', true);
}
// if 2nd argument is a number, wait n steps
if (checkReal(split[| 1]))
{
    var waiter = instance_create(0, 0, obj_stepWaiter);
    waiter.duration = real(split[| 1]);
    dialogueWait(waiter);
}

// if 2nd argument is "for"
if (split[| 1] == "for")
{
    if (ds_list_size(split) < 3)
    {
        show_error('Error in line "' + string(line) + '": wait for requires at least 1 argument: wait for something', true);
    }
    switch (split[| 2])
    {
        case "drawer":
        case "drawers":
        case "draw":
            var waiter = instance_create(0, 0, obj_drawerWaiter);
            dialogueWait(waiter);
            break;
    }
}

ds_list_destroy(split);
// it won't advance, since we're waiting
return true;
