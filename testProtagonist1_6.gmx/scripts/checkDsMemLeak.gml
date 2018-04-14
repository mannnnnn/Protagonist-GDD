///checkDsMemLeak()
// checks all data structure types for memory leaks

var n = 1000;

// lists
var lists = 0;
for (var i = 0; i < n; i++)
{
    if (ds_exists(i, ds_type_list))
    {
        lists++;
    }
}

// maps
var maps = 0;
for (var i = 0; i < n; i++)
{
    if (ds_exists(i, ds_type_map))
    {
        maps++;
    }
}

// stacks
var stacks = 0;
for (var i = 0; i < n; i++)
{
    if (ds_exists(i, ds_type_stack))
    {
        stacks++;
    }
}

// queues
var queues = 0;
for (var i = 0; i < n; i++)
{
    if (ds_exists(i, ds_type_queue))
    {
        queues++;
    }
}

// grids
var grids = 0;
for (var i = 0; i < n; i++)
{
    if (ds_exists(i, ds_type_grid))
    {
        grids++;
    }
}

// show
show_message(
"Lists: " + string(lists) + newline() + 
"Maps: " + string(maps) + newline() + 
"Stacks: " + string(stacks) + newline() + 
"Queues: " + string(queues) + newline() + 
"Grids: " + string(grids)
);
