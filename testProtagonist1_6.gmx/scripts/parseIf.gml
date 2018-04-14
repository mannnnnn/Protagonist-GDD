///parseIf(line, split)
var line = argument0;
var split = argument1;

var expression = argument0;
// get the variable name
// remove the if
expression = string_copy(expression, 3, string_length(line) - 3 + 1);

// evaluate the expression
var val = evalBoolean(expression);

// look ahead
var bracketCounter = 0;
var jumpPointer = noone;
if (val)
{
    jumpPointer = obj_dialogue.line + 1;
}
var els = false;
var returnPointer = noone;
for (var i = obj_dialogue.line; (i < ds_list_size(obj_dialogue.dialogue) && returnPointer == noone); i++)
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
    // find if and else
    if (bracketCounter == 0)
    {
        // if we're on the finishing bracket
        if (obj_dialogue.dialogue[| i] == "}")
        {
            // if we've already seen else, then we're done
            if (els)
            {
                returnPointer = i;
            }
            // if the next thing is else, then there's an else
            else if (i < ds_list_size(obj_dialogue.dialogue) - 1)
            {
                var l = obj_dialogue.dialogue[| i + 1];
                // check for else if fulfilled
                if (string_pos("else if", l) == 1)
                {
                    // set jump pointer if needed
                    if (jumpPointer == noone)
                    {
                        // get the variable name
                        // remove the else if
                        expression = string_copy(l, 8, string_length(l) - 8 + 1);
                        // evaluate the expression
                        val = evalBoolean(expression);
                        if (val)
                        {
                            jumpPointer = i + 1;
                        }
                    }
                }
                else if (l == "else")
                {
                    // set jump pointer if needed
                    if (jumpPointer == noone)
                    {
                        jumpPointer = i + 1;
                    }
                    els = true;
                }
                // if no else, then we're done here
                else
                {
                    returnPointer = i;
                }
            }
            else
            {
                returnPointer = i;
            }
        }
    }
}
if (returnPointer == noone)
{
    show_error("Error: Bracket mismatch.", true);
}

// if jumpPointer not set, then nothing satisfied
// jump to return pointer and don't push stack
if (jumpPointer == noone)
{
    obj_dialogue.line = returnPointer;
}
// if jumpPointer is set, then jump to that and push stack
else
{
    // push end of if statement onto stack of return pointers
    ds_stack_push(obj_dialogue.jumpstack, returnPointer);
    // jump to start point
    obj_dialogue.line = jumpPointer;
}

ds_list_destroy(split);
return false;
