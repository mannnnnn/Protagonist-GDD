///parseClosedBracket(line, split)
var line = argument0;
var split = argument1;

// check if stack is empty
if (ds_stack_empty(jumpstack))
{
    // show_error("Bracket mismatch.", true);
    // exit dialogue if end bracket and nothing to return to
    return parseExit("exit", split);
}
// pop the most recent return pointer off the jumpstack
var returnpointer = ds_stack_pop(jumpstack);
obj_dialogue.line = returnpointer;
// continue
ds_list_destroy(split);
return false;
